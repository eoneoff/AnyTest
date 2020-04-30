using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.IDataRepository;
using AnyTest.Model;
using AnyTest.Model.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyTest.DataService.Controllers
{
    ///<inheritdoc/>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ApiControllerBase<Test>
    {
        ///<inheritdoc/>
        public TestsController(IRepository<Test> repository) : base(repository)
        {
        }

        ///<inheritdoc />
        [Authorize(Roles = "Administratir, Tutor")]
        public override async Task<IActionResult> Post([FromBody] Test item) => await base.Post(item);

        ///<inheritdoc/>
        [Authorize(Roles ="Administrator, Tutor")]
        public override async Task<IActionResult> Put(long id, [FromBody] Test item) => await base.Put(id, item);

        ///<inheritdoc/>
        [Authorize(Roles = "Administrator, Tutor")]
        public override async Task<IActionResult> Delete(long id) => await base.Delete(id);

        /// <summary>
        /// \~english A method to gett a structured list of subjects, courses and tests
        /// \~ukrainian Метод для отримання структурованого списку предметів, курсів та тестів
        /// </summary>
        /// <returns>
        /// \~english A list of subjects, courses and tests, organized as a tree
        /// \~ukrainian Список предметів, курсів та тестів деревоподібної структури
        /// </returns>
        /// /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// GET: api/[controller]/list
        /// </code>
        /// </example>
        [HttpGet("list")]
        public async Task<Dictionary<string, List<TestsTreeModel>>> GetTestsList() => await (_repository as ITestRepository).GetTestsList();
    }
}