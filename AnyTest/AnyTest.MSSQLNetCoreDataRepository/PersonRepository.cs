using AnyTest.DbAccess;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AnyTestDbContext db) : base(db)
        {
        }

        public async Task<Person> FindByEmail(string email) => await _db.Set<Person>().AsNoTracking().SingleOrDefaultAsync(p => p.Email == email);
    }
}
