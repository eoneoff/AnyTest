using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyTest.DataService.Controllers
{
    /// <summary>
    /// \~english An API controller for people personal data
    /// \~ukrainian Контроллер API для особистих даних
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        /// <summary>
        /// \~english A repository for an access to people data
        /// \~ukrainian Репозіторій для доступу до особитисти даних
        /// </summary>
        private IPersonRepository _repository;

        /// <summary>
        /// \~english Initializes a new instance of the <c>PeopleContorller</c> class
        /// \~ukrainian Ініціалізує новий екземляр класу <c>PeopleContorller</c>
        /// </summary>
        /// <param name="repository">
        /// \~english An object, implementing an <c>IPersonRepository</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IPersonRepository</c>. Залежність.
        /// </param>
        public PeopleController(IPersonRepository repository) => _repository = repository;


        /// <summary>
        /// \~english A method for getting a complete list of people
        /// \~ukrainian Метод для отримання персональних даних всіх користувачів
        /// </summary>
        /// <returns>
        /// \~english A collection of <c>Person</c>
        /// \~ukrainian Коллекція <c>Person</c>
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request for people
        /// \~ukrainian Приклад HTTP запиту персональних даних всіх користуваів
        /// <code>
        /// GET: api/People
        /// </code>
        /// </example>
        [HttpGet]
        public async Task<IEnumerable<Person>> Get() => await _repository.Get();


        /// <summary>
        /// \~english A method for gettin a single person by id
        /// \~ukrainian Метод для отримання персональних даних користувача по id
        /// </summary>
        /// <param name="id">
        /// \~english User id <c>long</c>
        /// \~ukrainian Id пользователя <c>long</c>
        /// </param>
        /// <returns>
        /// <c>Person</c>
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request for a person
        /// \~ukrainian Приклад HTTP запиту персональних даних користувача
        /// <code>
        /// GET: api/People/5
        /// </code>
        /// </example>
        [HttpGet("{id:long}", Name = "Get")]
        public async Task<Person> Get(long id) => await _repository.Get(id);

        /// <summary>
        /// \~english Creates a new person
        /// \~ukrainian Додає персональні дані нового користувача
        /// </summary>
        /// <param name="person">
        /// \~english An instance of a <c>Person</c>, conatining new user data
        /// \~ukrainian Екземпляр класу <c>Person</c>, якій місить дані нового користувача
        /// </param>
        /// <returns>
        /// \~english A result of a person creation
        /// \~ukrainian Результат створення особистих даних
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to create a person
        /// \~ukrainian Приклад HTTP запиту створення особистих даних користувача
        /// <code>
        /// POST: api/People
        /// </code>
        /// </example>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _repository.Post(person));
        }

        /// <summary>
        /// \~english Edits an existing person
        /// \~ukrainian Змінює пресональні дані існуючого користувача
        /// </summary>
        /// <param name="id">
        /// \~english A <c>Person</c> id. <c>long</c>
        /// \~ukrainian Id користувача. <c>long</c>
        /// </param>
        /// <param name="person">
        /// \~english A <c>Person</c> object with updated person data
        /// \~ukrainian Об'єкт класту <c>Person</c> із зміненими особистими даними
        /// </param>
        /// <returns>
        /// \~english A result of person updating
        /// \~ukrainian Результат оновлення особистих даних користувача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to update a person
        /// \~ukrainian Приклад HTTP запиту зміни особистих даних користувача
        /// <code>
        /// PUT: api/People/5
        /// </code>
        /// </example>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Person person)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _repository.Exists(id))
            {
                return BadRequest("User does not exist");
            }

            return Ok(await _repository.Put(person));
        }


        /// <summary>
        /// \~english Deleted a user
        /// \~ukrainian Видаляє особисті дані користувача
        /// </summary>
        /// <param name="id">
        /// \~english User id <c>long</c>
        /// \~ukrainian Id користувача <c>long</c>
        /// </param>
        /// <returns>
        /// \~english A result of deleting a person
        /// \~ukrainian Результат видалення особистих даних користувача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to delete a person
        /// \~ukrainian Приклад HTTP запиту видалення особистих даних користувача
        /// <code>
        /// DELETE: api/ApiWithActions/5
        /// </code>
        /// </example>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            if(!await _repository.Exists(id))
            {
                return BadRequest("User does not exist");
            }

            return Ok(await _repository.Delete(id));
        }
    }
}
