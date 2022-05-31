using ContactService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _appDbContext;
        public ContactRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public void Add(Contact contact)
        {
            _appDbContext.Contacts.Add(contact);
        }
        public List<Contact> GetAllContacts()
        {
            return _appDbContext.Contacts.ToList();
        }
        public void Delete(int id)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact != null)
            {
                _appDbContext.Contacts.Remove(contact);
            }
        }

        public List<Contact> List(string personUuid)
        {
            return _appDbContext.Contacts.Where(x=>x.PersonUuid == personUuid).ToList();
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }
    }
}
