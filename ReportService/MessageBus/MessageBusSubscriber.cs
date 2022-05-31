using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Data;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportService.MessageBus
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        
        public MessageBusSubscriber(IConfiguration configuration, IServiceScopeFactory factory)
        {
            _configuration = configuration;
            _serviceScopeFactory = factory;
            InitializeRabbitMQ();
        }
        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"] };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());
                var reportUuid = JsonConvert.DeserializeObject<Report>(notificationMessage).Uuid;
                var response = CallGetAllContacts();
                var contentStream = response.Content.ReadAsStream();

                using var streamReader = new StreamReader(contentStream);
                using var jsonReader = new JsonTextReader(streamReader);

                var serializer = new JsonSerializer();

                var reportResults = GetReportResults(serializer.Deserialize<List<Contact>>(jsonReader), reportUuid);
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                    foreach (var item in reportResults)
                    {
                        dbContext.ReportResults.Add(item);
                    }
                    var report = dbContext.Reports.FirstOrDefault(x => x.Uuid == reportUuid);
                    report.ReportStatus = ReportStatus.Completed;                    
                    dbContext.SaveChanges();
                }                
                
            };


            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
        private List<ReportResult> GetReportResults(List<Contact> contacts, string reportUuid)
        {
            var locations = new Dictionary<string, List<string>>();
            var reportResults = new List<ReportResult>();
            foreach (var contact in contacts)
            {
                if (contact.ContactType != ContactType.Address)
                {
                    continue;
                }
                if (locations.ContainsKey(contact.Content))
                {
                    locations[contact.Content].Add(contact.PersonUuid);
                    continue;
                }
                locations.Add(contact.Content, new List<string>());
                locations[contact.Content].Add(contact.PersonUuid);
            }

            foreach (var address in locations.Keys)
            {
                var locationReportResult = new ReportResult();
                locationReportResult.ReportUuid = reportUuid;
                locationReportResult.Location = address;
                locationReportResult.PersonCount = locations[address].Count;
                locationReportResult.PhoneCount = contacts.Count(x => x.ContactType == ContactType.Phone && locations[address].Contains(x.PersonUuid));
                reportResults.Add(locationReportResult);
            }
            return reportResults;
        }
    
    private HttpResponseMessage CallGetAllContacts()
    {
        // Retrieve the user's To Do List.
        using (var client = new HttpClient())
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _configuration["GetAllContactsUrl"]);
            return client.Send(request);
        }
    }

    //private static async Task RunAsync()
    //{
    //    // Update your local service port no. / service APIs etc in the following line
    //    Client.BaseAddress = new Uri("http://localhost:57579/api/values/");
    //    Client.DefaultRequestHeaders.Accept.Clear();
    //    Client.DefaultRequestHeaders.Accept.Add(
    //       new MediaTypeWithQualityHeaderValue("application/json"));

    //    try
    //    {
    //        var items = await GetItems("http://localhost:57579/api/values/");
    //        Console.WriteLine("Items read using the web api GET");
    //        Console.WriteLine(string.Join(string.Empty, items.Aggregate((current, next) => current + ", " + next)));
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e.Message);
    //    }

    //    Console.ReadLine();
    //}
    public override void Dispose()
    {
        if (_connection.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        base.Dispose();
    }
}
}
