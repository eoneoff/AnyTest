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
    public class SubjectsRepository : Repository<Subject>
    {
        public SubjectsRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Subject>> Get() =>
            await _db.Subjects.Where(s => !s.Changed)
            .Include(s => s.Tests)
            .AsNoTracking().ToListAsync();

        public override async Task<Subject> Post(Subject item) => await base.Put(item);

        public override async Task<Subject> Put(Subject item, params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
                var old = await _db.Subjects.FindAsync(id);
                if (old == null) throw new ArgumentException("No subject with such id");

                old.Changed = true;
                _db.Update(old);
                item.Id = 0;
                _db.Add(item);
                await _db.SaveChangesAsync();
                return item;
            }
            else throw new ArgumentException("Subject id must be of type long");
        }
    }
}
