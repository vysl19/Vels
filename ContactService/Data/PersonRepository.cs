using ContactService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IContactRepository _contactRepository;
        public PersonRepository(AppDbContext context, IContactRepository contactRepository)
        {
            _appDbContext = context;
            _contactRepository = contactRepository;
        }
        public void Add(Person person)
        {
            var guid = Guid.NewGuid();
            person.Uuid = guid.ToString();
            _appDbContext.Persons.Add(person);
        }

        public void Delete(string uuid)
        {
            var person = _appDbContext.Persons.FirstOrDefault(x => x.Uuid == uuid);
            if (person != null)
            {
                _appDbContext.Persons.Remove(person);
            }
        }

        public Person Get(string uuid)
        {
            var person= _appDbContext.Persons.FirstOrDefault(x=>x.Uuid == uuid);
            person.Contacts = _contactRepository.List(uuid);
            return person;
        }

        public List<Person> List()
        {
            return _appDbContext.Persons.ToList();
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
}
