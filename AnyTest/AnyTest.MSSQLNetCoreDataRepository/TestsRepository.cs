using AnyTest.DbAccess;
using AnyTest.IDataRepository;
using AnyTest.Model;
using AnyTest.Model.ApiModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class TestsRepository : Repository<Test>, ITestRepository
    {
        public TestsRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Test>> Get() =>
            await _db.Tests.Include(t => t.Subjects)
            .Include(t => t.Courses)
            .Include(t => t.TestQuestions).ThenInclude(q => q.TestAnswers)
            .AsNoTracking().ToListAsync();

        public override async Task<Test> Get(params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
                return await _db.Tests.Include(t => t.Subjects)
                .Include(t => t.Courses)
                .Include(t => t.TestQuestions).ThenInclude(q => q.TestAnswers)
                .AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            }
            else throw new ArgumentException("Test Id must be of type long");
        }

        public async Task<Dictionary<string, List<TestsTreeModel>>> GetTestsList()
        {
            var result = new Dictionary<string, List<TestsTreeModel>>
            {
                {"subjects", new List<TestsTreeModel>() },
                {"courses", new List<TestsTreeModel>() },
                {"tests", new List<TestsTreeModel>() }
            };

            var subjects = await _db.Subjects.Include(s => s.Courses).ThenInclude(c => c.Tests).ThenInclude(t => t.Test)
                .Include(s => s.Tests).ThenInclude(t => t.Test).AsNoTracking().ToListAsync();
            result["subjects"] = subjects.Select(s => new TestsTreeModel
            {
                Id = s.Id,
                Name = s.Name,
                Url = $"subjects/{s.Id}",
                Children = s.Courses.Select(c => new TestsTreeModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = $"courses/{c.Id}",
                    Children = c.Tests.Select(t => new TestsTreeModel
                    {
                        Id = t.TestId,
                        Name = t.Test.Name,
                        Url = $"tests/{t.TestId}"
                    })
                }).Concat(s.Tests.Where(t => !s.Courses.SelectMany(c => c.Tests).Any(tc => tc.TestId == t.TestId)).Select(tc => new TestsTreeModel
                {
                    Id = tc.TestId,
                    Name = tc.Test.Name,
                    Url = $"tests/{tc.TestId}"
                }))
            }).ToList();

            var courses = await _db.Courses.Include(c => c.Tests).ThenInclude(t => t.Test).AsNoTracking().ToListAsync();
            result["courses"] = courses.Select(c => new TestsTreeModel
            {
                Id = c.Id,
                Name = c.Name,
                Url = $"courses/{c.Id}",
                Children = c.Tests.Select(t => new TestsTreeModel
                {
                    Id = t.TestId,
                    Name = t.Test.Name,
                    Url = $"tests/{t.TestId}"
                })
            }).ToList();

            var tests = await _db.Tests.AsNoTracking().ToListAsync();
            result["tests"] = tests.Select(t => new TestsTreeModel
            {
                Id = t.Id,
                Name = t.Name,
                Url = $"tests/{t.Id}"
            }).ToList();

            return result;
        }
    }
}
