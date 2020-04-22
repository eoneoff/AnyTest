using AnyTest.IDataRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnyTest.DataService.Controllers
{
    /// <summary>
    /// \~english An API controller with CRUD methods
    /// \~ukrainian Контроллер API з методами CRUD
    /// </summary>
    [ApiController]
    public class ApiControllerBase<T> : ControllerBase
    {
        private IRepository<T> _repository;

        /// <summary>
        /// \~english Initializes a new instance of the controller
        /// \~ukrainian Ініціалізує новий екземляр контроллеру
        /// </summary>
        /// <param name="repository">
        /// \~english An object, implementing an <c>IRepository{T}</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IRepository{T}</c>. Залежність.
        /// </param>
        public ApiControllerBase(IRepository<T> repository) => _repository = repository;

        /// <summary>
        /// \~english A method for getting all data
        /// \~ukrainian Метод для отримання всіх даних
        /// </summary>
        /// <returns>
        /// \~english A collection of <c>T</c>
        /// \~ukrainian Коллекція <c>T</c>
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// GET: api/[controller]
        /// </code>
        /// </example>
        [HttpGet]
        public virtual async Task<IEnumerable<T>> Get() => await _repository.Get();

        /// <summary>
        /// \~english A method for gettin a single item by id
        /// \~ukrainian Метод для отримання одного об'єкту по id
        /// </summary>
        /// <param name="id">
        /// \~english Id <c>long</c>
        /// \~ukrainian Id <c>long</c>
        /// </param>
        /// <returns>
        /// <c>tutor</c>
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// GET: api/[controller]/5
        /// </code>
        /// </example>
        [HttpGet("{id:long}")]
        public virtual async Task<T> Get(long id) => await _repository.Get(id);

        /// <summary>
        /// \~english Creates a new item
        /// \~ukrainian Додає нових об'єкт
        /// </summary>
        /// <param name="item">
        /// \~english An instance of a <c>T</c>
        /// \~ukrainian Екземпляр класу <c>T</c>
        /// </param>
        /// <returns>
        /// \~english A result of creation
        /// \~ukrainian Результат створення
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// POST: api/[controller]
        /// </code>
        /// </example>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]T item)
        {
            if (!ModelState.IsValid)
            {
                return (BadRequest(ModelState));
            }

            return Ok(await _repository.Post(item));
        }

        /// <summary>
        /// \~english Edits an existing item
        /// \~ukrainian Змінює дані існуючого об'єктур
        /// </summary>
        /// <param name="id">
        /// \~english A <c>T</c> id. <c>long</c>
        /// \~ukrainian Id <c>long</c>
        /// </param>
        /// <param name="item">
        /// \~english A <c>T</c> object with updated data
        /// \~ukrainian Об'єкт класу <c>T</c> із зміненими даними
        /// </param>
        /// <returns>
        /// \~english A result of updating
        /// \~ukrainian Результат оновлення
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// PUT: api/[controller]/5
        /// </code>
        /// </example>
        [HttpPut("{id:long}")]
        public virtual async Task<IActionResult> Put(long id, [FromBody] T item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repository.Exists(id))
            {
                return BadRequest("Tutor does not exist");
            }

            return Ok(await _repository.Put(item));
        }


        /// <summary>
        /// \~english Deletes a object
        /// \~ukrainian Видаляє дані
        /// </summary>
        /// <param name="id">
        /// \~english Id <c>long</c>
        /// \~ukrainian Id <c>long</c>
        /// </param>
        /// <returns>
        /// \~english A result of deleting
        /// \~ukrainian Результат видалення
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// DELETE: api/[controller]/5
        /// </code>
        /// </example>
        [HttpDelete("{id:long}")]
        public virtual async Task<IActionResult> Delete (long id)
        {
            if (!await _repository.Exists(id))
            {
                return BadRequest("User does not exist");
            }

            return Ok(await _repository.Delete(id));
        }
    }
}
