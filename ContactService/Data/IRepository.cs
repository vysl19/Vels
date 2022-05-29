using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public interface IRepository
    {
        void SaveChanges();
    }
}
