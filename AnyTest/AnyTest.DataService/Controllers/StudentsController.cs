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
        [Authorize(Roles="Administrator")]
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

         private async Task<bool> IsOwner(long id)
        {
            var userEmail = (HttpContext.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Email).Value;
            var personEmail = (await _people.Get(id)).Email;

            return userEmail == personEmail;
        }
    }
}