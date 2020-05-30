using AnyTest.DbAccess;
using AnyTest.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class TestPassesRepository : Repository<TestPass>
    {
        public TestPassesRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<TestPass> Get(params object[] key)
        {
            if(key.Length > 0 && key[0] is long id)
            {
                return await _db.TestPasses.Include(p => p.Answers).AsTracking().SingleOrDefaultAsync(p => p.Id == id);
            }
            throw new ArgumentException("Get TestPass requires an Id of type long");
        }
    }
}
