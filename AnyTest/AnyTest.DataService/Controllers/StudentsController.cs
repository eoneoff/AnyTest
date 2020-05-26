using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnyTest.DataService.Controllers
{
     /// <summary>
    /// \~english An API controller Students data
    /// \~ukrainian Контроллер API для даних студентів
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IRepository<Student> _repository;
        private IPersonRepository _people;

        /// <summary>
        /// \~english Initializes a new instance of the <c>StudentsContorller</c> class
        /// \~ukrainian Ініціалізує новий екземляр класу <c>StudentsContorller</c>
        /// </summary>
        /// <param name="repository">
        /// \~english An object, implementing an <c>IRepository{Student}</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IRepository{Student}</c>. Залежність.
        /// </param>
        ///<param name="people">
        /// \~english An object, implementing an <c>IPeopleRepositor</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IPeopleRepository</c>. Залежність.
        /// </param>
        public StudentsController(IRepository<Student> repository, IPersonRepository people) => (_repository, _people) = (repository, people);

        /// <summary>
        /// \~english A method for getting a complete list of Students
        /// \~ukrainian Метод для отримання персональних даних всіх студентів
        /// </summary>
        /// <returns>
        /// \~english A collection of <c>Student</c>
        /// \~ukrainian Коллекція <c>Student</c>
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request for Students
        /// \~ukrainian Приклад HTTP запиту персональних даних всіх студентів
        /// <code>
        /// GET: api/Students
        /// </code>
        /// </example>
        [HttpGet]
        [Authorize(Roles="Administrator, Tutor")]
        public async Task<IEnumerable<Student>> Get() => await _repository.Get();

        /// <summary>
        /// \~english A method for gettin a single Student by id
        /// \~ukrainian Метод для отримання персональних даних студента по id
        /// </summary>
        /// <param name="id">
        /// \~english Student id <c>long</c>
        /// \~ukrainian Id студента <c>long</c>
        /// </param>
        /// <returns>
        /// <c>Student</c>
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request for a Student
        /// \~ukrainian Приклад HTTP запиту персональних даних студента
        /// <code>
        /// GET: api/Students/5
        /// </code>
        /// </example>
        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<Student> Get(long id) => await _repository.Get(id);

        /// <summary>
        /// \~english Creates a new Student
        /// \~ukrainian Додає персональні дані нового студента
        /// </summary>
        /// <param name="Student">
        /// \~english An instance of a <c>Student</c>, conatining new user data
        /// \~ukrainian Екземпляр класу <c>Student</c>, якій місить дані нового студента
        /// </param>
        /// <returns>
        /// \~english A result of a Student creation
        /// \~ukrainian Результат створення студента
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to create a Student
        /// \~ukrainian Приклад HTTP запиту створення студента студента
        /// <code>
        /// POST: api/Students
        /// </code>
        /// </example>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Student Student)
        {
            if(!ModelState.IsValid)
            {
                return(BadRequest(ModelState));
            }

            return Ok(await _repository.Post(Student));
        }

        /// <summary>
        /// \~english Edits an existing Student
        /// \~ukrainian Змінює пресональні дані існуючого студента
        /// </summary>
        /// <param name="id">
        /// \~english A <c>Student</c> id. <c>long</c>
        /// \~ukrainian Id студента. <c>long</c>
        /// </param>
        /// <param name="Student">
        /// \~english A <c>Student</c> object with updated Student data
        /// \~ukrainian Об'єкт класту <c>Student</c> із зміненими особистими даними
        /// </param>
        /// <returns>
        /// \~english A result of Student updating
        /// \~ukrainian Результат оновлення студента
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to update a Student
        /// \~ukrainian Приклад HTTP запиту зміни студента
        /// <code>
        /// PUT: api/Students/5
        /// </code>
        /// </example>
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Administrator, Student")]
        public async Task<IActionResult> Put(long id, [FromBody] Student Student)
        {
            if (!await IsOwner(id) && !HttpContext.User.IsInRole("Administrator")) return Unauthorized();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest("Student does not exist");
            }

            return Ok(await _repository.Put(Student));
        }

        /// <summary>
        /// \~english Deleted a Student
        /// \~ukrainian Видаляє особисті дані студентаа
        /// </summary>
        /// <param name="id">
        /// \~english Student id <c>long</c>
        /// \~ukrainian Id студента <c>long</c>
        /// </param>
        /// <returns>
        /// \~english A result of deleting a Student
        /// \~ukrainian Результат видалення особистих даних студента
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to delete a Student
        /// \~ukrainian Приклад HTTP запиту видалення особистих даних студента
        /// <code>
        /// DELETE: api/Students/5
        /// </code>
        /// </example>
        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await IsOwner(id) && !HttpContext.User.IsInRole("Administrator")) return Unauthorized();

            if(!await _repository.Exists(id))
            {
                return BadRequest("User does not exist");
            }

            return Ok(await _repository.Delete(id));
        }

        /// <summary>
        /// \~english Returns paginaged list of stucents
        /// \~ukrainian Повертає список студентів, розбитий на сторінки
        /// </summary>
        /// <param name="pageNumber">
        /// \~english page number
        /// \~ukrainian Номер сторінки
        /// </param>
        /// <param name="pageSize">
        /// \~english Page size
        /// \~ Розмір сторінки
        /// </param>
        /// <returns>
        /// \~english A page from list of students
        /// \~ukrainian Сторінка списку студентів
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request got paginated list of students
        /// \~ukrainian Приклад HTTP запиту списку студентів, розбитого на сторінки
        /// <code>
        /// GET: api/Students/page/3/12
        /// </code>
        /// </example>
        [HttpGet("page/{pageNumber:int}/{pageSize:int}")]
        public async Task<IEnumerable<Student>> GetStudentsPage(int pageNumber, int pageSize) => await (_repository as IStudentsRepository).GetStudentPage(pageNumber, pageSize);

        /// <summary>
        /// \~english Adds a student to course
        /// \~ukrainian Додає студента до курсу
        /// </summary>
        /// <param name="id">
        /// \~english A <c>Student</c> id. <c>long</c>
        /// \~ukrainian Id студента. <c>long</c>
        /// </param>
        /// <param name="courseId">
        /// \~english A course id
        /// \~ukrainian Id курсу
        /// </param>
        /// <returns>
        /// \~english An object of student-course connection
        /// \~ukrainian Об'єкт зв'язку студента і курсу
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to add student to course
        /// \~ukrainian Приклад HTTP запиту додавання студента до курсу
        /// <code>
        /// POST: api/Students/5/Course/3
        /// </code>
        /// </example>
        [HttpPost("{id:long}/courses/{courseId:long}")]
        public async Task<IActionResult> AddStudentToCourse(long id, long courseId)
        {
            if (!await _repository.Exists(id)) return BadRequest();
            return Ok(await (_repository as IStudentsRepository).AddToCourse(id, courseId));
        }

        /// <summary>
        /// \~english Removes a student to course
        /// \~ukrainian Видаляє студента з курсу
        /// </summary>
        /// <param name="id">
        /// \~english A <c>Student</c> id. <c>long</c>
        /// \~ukrainian Id студента. <c>long</c>
        /// </param>
        /// <param name="courseId">
        /// \~english A course id
        /// \~ukrainian Id курсу
        /// </param>
        /// <returns>
        /// \~english An object of student-course connection
        /// \~ukrainian Об'єкт зв'язку студента і курсу
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to remove a student to course
        /// \~ukrainian Приклад HTTP запиту видалення студента до курсу
        /// <code>
        /// DELETE: api/Students/5/Course/3
        /// </code>
        /// </example>
        [HttpDelete("{id:long}/courses/{courseId:long}")]
        public async Task<IActionResult> RemoveStudentFromCourse(long id, long courseId)
        {
            if (!await _repository.Exists(id)) return BadRequest();
            return (Ok(await (_repository as IStudentsRepository).RemoveFromCourse(id, courseId)));
        }

        /// <summary>
        /// \~english Adds a test pass to a student
        /// \~ukrainian Додає проходження тесту студентом
        /// </summary>
        /// <param name=studentId">
        /// \~english An id of a <c>Student</c>
        /// \~ukrainian Id студенту
        /// </param>
        /// <param name="pass">
        /// \~english A test pass
        /// \~ukrainian Проходження тесту
        /// </param>
        /// <returns>
        /// \~english A test pass
        /// \~ukrainian Проходження тесту
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to save a test pass
        /// \~ukrainian Приклад HTTP запиту збереження проходження тесту
        /// <code>
        /// POST: api/Students/5/tests
        /// </code>
        /// </example>
        [HttpPost("{studentId:long}/tests/")]
        public async Task<IActionResult> AddTestPassToStudent(long studentId, [FromBody] TestPass pass)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (! await _repository.Exists(studentId)) return BadRequest();
            pass.StudentId = studentId;
            return Ok(await (_repository as IStudentsRepository)?.SavePass(pass));
        }

        private async Task<bool> IsOwner(long id)
        {
            var userEmail = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            var personEmail = (await _people.Get(id)).Email;

            return userEmail == personEmail;
        }
    }
}