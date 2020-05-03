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
            .Where(t => !t.Changed)
            .Include(t => t.Courses)
            .Include(t => t.TestQuestions).ThenInclude(q => q.TestAnswers)
            .AsNoTracking().ToListAsync();

        public override async Task<Test> Get(params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
               return  await _db.Tests
                    .Include(t => t.Subjects)
                    .Include(t => t.Courses)
                    .Include(t => t.TestQuestions).ThenInclude(q => q.TestAnswers)
                    .AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            }
            else throw new ArgumentException("Test Id must be of type long");
        }

        public override async Task<Test> Put(Test item, params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
                var old = await _db.Tests.FindAsync(id);
                if (old == null) throw new ArgumentException("No test with such id");

                old.Changed = true;
                _db.Update(old);
                item.Id = 0;
                foreach (var question in item.TestQuestions)
                {
                    question.Id = 0;
                    question.TestId = 0;
                    foreach(var answer in question.TestAnswers)
                    {
                        answer.Id = 0;
                        answer.TestQuestionId = 0;
                    }
                }
                foreach(var subject in item.Subjects) subject.TestId = 0;
                foreach(var course in item.Courses) course.TestId = 0;
                _db.Add(item);
                await _db.SaveChangesAsync();
                return item;
            }
            else throw new ArgumentException("Test id must be of type long");
        }

        public async Task<Dictionary<string, List<TestsTreeModel>>> GetTestsList()
        {
            var result = new Dictionary<string, List<TestsTreeModel>>
            {
                {"subjects", new List<TestsTreeModel>() },
                {"courses", new List<TestsTreeModel>() },
                {"tests", new List<TestsTreeModel>() }
            };

            var subjects = await _db.Subjects.Where(s => !s.Changed).Include(s => s.Courses).ThenInclude(c => c.Tests).ThenInclude(t => t.Test)
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

            var courses = await _db.Courses.Where(c => !c.Changed).Include(c => c.Tests).ThenInclude(t => t.Test).AsNoTracking().ToListAsync();
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

            var tests = await _db.Tests.Where(t => !t.Changed).AsNoTracking().ToListAsync();
            result["tests"] = tests.Select(t => new TestsTreeModel
            {
                Id = t.Id,
                Name = t.Name,
                Url = $"tests/{t.Id}"
            }).ToList();

            return result;
        }

        public async Task<TestSubject> AddSubject(long testId, long subjectId)
        {
            var testSubject = _db.TestSubjects.Find(testId, subjectId);
            if(testSubject == null)
            {
                testSubject = new TestSubject { TestId = testId, SubjectId = subjectId };
                _db.Add(testSubject);
                await _db.SaveChangesAsync();

            }
            else if (testSubject.Deleted)
            {
                testSubject.Deleted = false;
                _db.Update(testSubject);
                await _db.SaveChangesAsync();
            }

            _db.Entry(testSubject).State = EntityState.Detached;
            return testSubject;
        }

        public async Task<TestSubject> RemoveSubject(long testId, long subjectId)
        {
            var testSubject = _db.TestSubjects.Find(testId, subjectId);
            if(testSubject != null)
            {
                testSubject.Deleted = true;
                _db.Update(testSubject);
                await _db.SaveChangesAsync();
                _db.Entry(testSubject).State = EntityState.Detached;
            }

            return testSubject;
        }

        public async Task<TestCourse> AddCourse(long testId, long courseId)
        {
            var testCourse = _db.TestCourses.Find(testId, courseId);
            if (testCourse == null)
            {
                testCourse = new TestCourse { TestId = testId, CourseId = courseId };
                _db.Add(testCourse);
                await _db.SaveChangesAsync();

            }
            else if (testCourse.Deleted)
            {
                testCourse.Deleted = false;
                _db.Update(testCourse);
                await _db.SaveChangesAsync();
            }

            _db.Entry(testCourse).State = EntityState.Detached;
            return testCourse;
        }

        public async Task<TestCourse> RemoveCourse(long testId, long courseId)
        {
            var testCourse = _db.TestCourses.Find(testId, courseId);
            if (testCourse != null)
            {
                testCourse.Deleted = true;
                _db.Update(testCourse);
                await _db.SaveChangesAsync();
                _db.Entry(testCourse).State = EntityState.Detached;
            }

            return testCourse;
        }
    }
}
