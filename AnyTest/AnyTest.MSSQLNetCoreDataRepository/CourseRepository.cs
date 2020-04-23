using AnyTest.DbAccess;
using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class CourseRepository : Repository<Course>
    {
        public CourseRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Course>> Get() => await _db.Courses
            .Include(c => c.Tests)
            .Include(c => c.Owners)
            .ToListAsync();
    }
}
