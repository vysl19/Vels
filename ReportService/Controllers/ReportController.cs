using Microsoft.AspNetCore.Mvc;
using ReportService.Data;
using ReportService.MessageBus;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMessageBusClient _messageBusClient;
        public ReportController(IReportRepository reportRepository, IMessageBusClient messageBusClient)
        {
            _reportRepository = reportRepository;
            _messageBusClient = messageBusClient;
        }
        // GET: api/<ReportController>
        [HttpGet]
        public ActionResult<IEnumerable<Report>> Get()
        {
            var reports = _reportRepository.Get();
            return Ok(reports);
        }

        // GET api/<ReportController>/5
        [HttpGet("{id}")]
        public ActionResult<Report> Get(string id)
        {
            return Ok(_reportRepository.Get(id));
        }

        // POST api/<ReportController>
        [HttpPost]
        public void Post()
        {
            var report = new Report()
            {
                Date = DateTime.Now,
                Uuid = Guid.NewGuid().ToString(),
                ReportStatus = ReportStatus.Preparing
            };
            _reportRepository.Add(report);
            _messageBusClient.CreateReport(report);
            _reportRepository.SaveChanges();
        }

    }
}
