using ContactService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        public static void SeedData(AppDbContext context)
        {
            if (!context.Persons.Any())
            {
                context.Persons.Add(new Person() { Company = "Paycore", Name = "Veysel", Surname = "Kurhan", Uuid = "1" });
                context.Persons.Add(new Person() { Company = "Rise Technology", Name = "Deneme", Surname = "DenemeSon", Uuid = "2" });
                context.Contacts.Add(new Contact() { ContactType =ContactType.Phone, Content = "901234567878", PersonUuid ="1"});
                context.Contacts.Add(new Contact() { ContactType = ContactType.Email, Content = "veysel@gmail.com", PersonUuid = "1" });
                context.Contacts.Add(new Contact() { ContactType = ContactType.Address, Content = "Istanbul", PersonUuid = "1" });
                context.Contacts.Add(new Contact() { ContactType = ContactType.Phone, Content = "907894561212", PersonUuid = "2" });
                context.Contacts.Add(new Contact() { ContactType = ContactType.Email, Content = "denemeson@gmail.com", PersonUuid = "2" });
                context.Contacts.Add(new Contact() { ContactType = ContactType.Address, Content = "Ankara", PersonUuid = "2" });
                context.SaveChanges();
            }
        }
    }
}
