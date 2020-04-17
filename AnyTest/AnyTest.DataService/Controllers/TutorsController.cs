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
    /// \~english An API controller tutors data
    /// \~ukrainian Контроллер API для даних викладачів
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private IRepository<Tutor> _repository;
        private IPersonRepository _people;

        /// <summary>
        /// \~english Initializes a new instance of the <c>TutorsContorller</c> class
        /// \~ukrainian Ініціалізує новий екземляр класу <c>TutorsContorller</c>
        /// </summary>
        /// <param name="repository">
        /// \~english An object, implementing an <c>IRepository{Tutor}</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IRepository{Tutor}</c>. Залежність.
        /// </param>
        ///<param name="people">
        /// \~english An object, implementing an <c>IPeopleRepositor</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IPeopleRepository</c>. Залежність.
        /// </param>
        public TutorsController(IRepository<Tutor> repository, IPersonRepository people) => (_repository, _people) = (repository, people);

        /// <summary>
        /// \~english A method for getting a complete list of tutors
        /// \~ukrainian Метод для отримання персональних даних всіх викладачів
        /// </summary>
        /// <returns>
        /// \~english A collection of <c>Tutor</c>
        /// \~ukrainian Коллекція <c>Tutor</c>
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request for tutors
        /// \~ukrainian Приклад HTTP запиту персональних даних всіх викладачів
        /// <code>
        /// GET: api/Tutors
        /// </code>
        /// </example>
        [HttpGet]
        [Authorize(Roles="Administrator")]
        public async Task<IEnumerable<Tutor>> Get() => await _repository.Get();

        /// <summary>
        /// \~english A method for gettin a single tutor by id
        /// \~ukrainian Метод для отримання персональних даних викладача по id
        /// </summary>
        /// <param name="id">
        /// \~english Tutor id <c>long</c>
        /// \~ukrainian Id викладача <c>long</c>
        /// </param>
        /// <returns>
        /// <c>tutor</c>
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request for a tutor
        /// \~ukrainian Приклад HTTP запиту персональних даних викладача
        /// <code>
        /// GET: api/Tutors/5
        /// </code>
        /// </example>
        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<Tutor> Get(long id) => await _repository.Get(id);

        /// <summary>
        /// \~english Creates a new tutor
        /// \~ukrainian Додає персональні дані нового викладача
        /// </summary>
        /// <param name="tutor">
        /// \~english An instance of a <c>Tutor</c>, conatining new user data
        /// \~ukrainian Екземпляр класу <c>Tutor</c>, якій місить дані нового викладача
        /// </param>
        /// <returns>
        /// \~english A result of a tutor creation
        /// \~ukrainian Результат створення викладача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to create a tutor
        /// \~ukrainian Приклад HTTP запиту створення викладача викладача
        /// <code>
        /// POST: api/Tutors
        /// </code>
        /// </example>
        [HttpPost]
        [Authorize(Roles="Tutor, Administrator")]
        public async Task<IActionResult> Post(Tutor tutor)
        {
            if(!ModelState.IsValid)
            {
                return(BadRequest(ModelState));
            }

            return Ok(await _repository.Post(tutor));
        }

        /// <summary>
        /// \~english Edits an existing tutor
        /// \~ukrainian Змінює пресональні дані існуючого викладача
        /// </summary>
        /// <param name="id">
        /// \~english A <c>Tutor</c> id. <c>long</c>
        /// \~ukrainian Id викладача. <c>long</c>
        /// </param>
        /// <param name="tutor">
        /// \~english A <c>Tutor</c> object with updated tutor data
        /// \~ukrainian Об'єкт класту <c>Tutor</c> із зміненими особистими даними
        /// </param>
        /// <returns>
        /// \~english A result of tutor updating
        /// \~ukrainian Результат оновлення викладача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to update a tutor
        /// \~ukrainian Приклад HTTP запиту зміни викладача
        /// <code>
        /// PUT: api/Tutors/5
        /// </code>
        /// </example>
        [HttpPut("{id:long}")]
        [Authorize(Roles = "Administrator, Tutor")]
        public async Task<IActionResult> Put(long id, [FromBody] Tutor tutor)
        {
            if (!await IsOwner(id) && !HttpContext.User.IsInRole("Administrator")) return Unauthorized();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest("Tutor does not exist");
            }

            return Ok(await _repository.Put(tutor));
        }

        /// <summary>
        /// \~english Deleted a tutor
        /// \~ukrainian Видаляє особисті дані викладачаа
        /// </summary>
        /// <param name="id">
        /// \~english Tutor id <c>long</c>
        /// \~ukrainian Id викладача <c>long</c>
        /// </param>
        /// <returns>
        /// \~english A result of deleting a tutor
        /// \~ukrainian Результат видалення особистих даних викладача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to delete a tutor
        /// \~ukrainian Приклад HTTP запиту видалення особистих даних викладача
        /// <code>
        /// DELETE: api/Tutors/5
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