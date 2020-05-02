using AnyTest.DbAccess;
using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            .Where(c => !c.Changed)
            .Include(c => c.Tests)
            .Include(c => c.Owners)
            .AsNoTracking().ToListAsync();

        public override async Task<Course> Post(Course item) => await base.Put(item);

        public override async Task<Course> Put(Course item, params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
                var old = await _db.Courses.FindAsync(id);
                if (old == null) throw new ArgumentException("No course with such id");

                old.Changed = true;
                _db.Update(old);
                item.Id = 0;
                _db.Add(item);
                await _db.SaveChangesAsync();
                return item;
            }
            else throw new ArgumentException("Course id must be of type long");
        }
    }
}
