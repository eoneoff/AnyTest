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
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        public StudentsRepository(AnyTestDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Student>> Get() => await _db.Students
            .Include(s => s.Person)
            .Include(s => s.Courses)
            .AsNoTracking().ToListAsync();

        public override async Task<Student> Get(params object[] key)
        {
            if (key.Length > 0 && key[0] is long id)
            {
                return await _db.Students
                    .Include(s => s.Person)
                    .Include(s => s.Courses)
                    .AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
            }
            else
                throw new ArgumentException("Student Id must be of type long");
        }

        public async Task<IEnumerable<Student>> GetStudentPage(int pageNubmer, int pageSize) =>
            await _db.Students.Include(s => s.Person)
            .Include(s => s.Courses)
            .OrderBy(s => s.Person.FirstName)
            .Skip(pageSize * (pageNubmer - 1))
            .Take(pageSize)
            .AsNoTracking().ToListAsync();

        public async Task<StudentCourse> AddToCourse(long studentId, long courseId)
        {
            var studentCourse = _db.StudentCourses.Find(studentId, courseId);
            if(studentCourse == null)
            {
                studentCourse = new StudentCourse { StudentId = studentId, CourseId = courseId };
                _db.Add(studentCourse);
                
            }
            else if(studentCourse.Removed)
            {
                studentCourse.Removed = false;
                _db.Update(studentCourse);
            }
            await _db.SaveChangesAsync();
            _db.Entry(studentCourse).State = EntityState.Detached;
            return studentCourse;
        }

        public async Task<StudentCourse> RemoveFromCourse(long studentId, long courseId)
        {
            var studentCoures = _db.StudentCourses.Find(studentId, courseId);
            studentCoures.Removed = true;
            _db.Update(studentCoures);
            await _db.SaveChangesAsync();
            return studentCoures;
        }
    }
}
