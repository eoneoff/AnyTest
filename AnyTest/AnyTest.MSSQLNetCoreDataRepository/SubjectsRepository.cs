using AnyTest.DbAccess;
using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class SubjectsRepository : Repository<Subject>
    {
        public SubjectsRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Subject>> Get() =>
            await _db.Subjects.Include(s => s.Tests)
            .AsNoTracking().ToListAsync();
    }
}
