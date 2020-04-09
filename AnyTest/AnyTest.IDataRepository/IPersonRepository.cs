using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> FindByEmail(string email);
    }
}
