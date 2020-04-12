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
    /// \~english An API controller tutors data
    /// \~ukrainian Контроллер API для даних викладачів
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private IRepository<Tutor> _repository;

        /// <summary>
        /// \~english Initializes a new instance of the <c>TutorsContorller</c> class
        /// \~ukrainian Ініціалізує новий екземляр класу <c>TutorsContorller</c>
        /// </summary>
        /// <param name="repository">
        /// \~english An object, implementing an <c>IRepository{Tutor}</c> interface. Dependency.
        /// \~ukrainian Об'єкт, який іплементує інтерфейс <c>IRepository{Tutor}</c>. Залежність.
        /// </param>
        public TutorsController(IRepository<Tutor> repository) => _repository = repository;

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
        /// <c>Person</c>
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request for a tutor
        /// \~ukrainian Приклад HTTP запиту персональних даних викладача
        /// <code>
        /// GET: api/Tutors/5
        /// </code>
        /// </example>
        [HttpGet("{id:long}")]
        public async Task<Tutor> Get(long id) => await _repository.Get(id);
    }
}