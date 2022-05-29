using ContactService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public interface IPersonRepository : IRepository
    {
        void Add(Person person);
        Person Get(string uuid);
        List<Person> List();
        void Delete(string uuid);

    }
}
