using ContactService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public interface IContactRepository:IRepository
    {
        void Add(Contact contact);

        void Delete(int id);
        List<Contact> List(string personUuid);
        List<Contact> GetAllContacts();
    }
}
