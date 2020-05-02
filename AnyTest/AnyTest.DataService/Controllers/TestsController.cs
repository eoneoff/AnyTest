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
        /// GET: api/tests/list
        /// </code>
        /// </example>
        [HttpGet("list")]
        public async Task<Dictionary<string, List<TestsTreeModel>>> GetTestsList() => await (_repository as ITestRepository).GetTestsList();

        /// <summary>
        /// \~english Adds a test to a subject
        /// \~ukrainian Додає тест до предмету
        /// </summary>
        /// <param name="testId">
        /// \~english Test id
        /// \~ukrainian Id тесту
        /// </param>
        /// <param name="subjectId">
        /// \~english Subject id
        /// \~ukrainian Id предмету
        /// </param>
        /// <returns>
        /// \~english a test-subject connection
        /// \~ukrainian зв'язок між тестом та предметом
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// POST: api/tests/5/subjects/2
        /// </code>
        /// </example>
        [HttpPost("{testId:long}/subjects/{subjectId:long}")]
        [Authorize(Roles = "Administrator, Tutor")]
        public async Task<IActionResult> AddSubject(long testId, long subjectId)
        {
            if (!await _repository.Exists(testId)) return BadRequest("Such test does not exist");

            return Ok(await (_repository as ITestRepository).AddSubject(testId, subjectId));
        }

        /// <summary>
        /// \~english Removes a test from a subject
        /// \~ukrainian Видаляє тест з предмету
        /// </summary>
        /// <param name="testId">
        /// \~english Test id
        /// \~ukrainian Id тесту
        /// </param>
        /// <param name="subjectId">
        /// \~english Subject id
        /// \~ukrainian Id предмету
        /// </param>
        /// <returns>
        /// \~english a test-subject connection
        /// \~ukrainian зв'язок між тестом та предметом
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// DELETE: api/tests/5/subjects/2
        /// </code>
        /// </example>
        [HttpDelete("{testId:long}/subjects/{subjectId:long}")]
        [Authorize(Roles = "Administrator, Tutor")]
        public async Task<IActionResult> RemoveSubject(long testId, long subjectId)
        {
            if (!await _repository.Exists(testId)) return BadRequest("Such test does not exist");
            var deleted = await (_repository as ITestRepository).RemoveSubject(testId, subjectId);
            if (!deleted.Deleted) return BadRequest("The test does not belong to such subject");
            return Ok(deleted);
        }

        /// <summary>
        /// \~english Adds a test to a course
        /// \~ukrainian Додає тест до курсу
        /// </summary>
        /// <param name="testId">
        /// \~english Test id
        /// \~ukrainian Id тесту
        /// </param>
        /// <param name="courseId">
        /// \~english Course id
        /// \~ukrainian Id курсу
        /// </param>
        /// <returns>
        /// \~english a test-course connection
        /// \~ukrainian зв'язок між тестом та курсом
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// POST: api/tests/5/courses/2
        /// </code>
        /// </example>
        [HttpPost("{testId:long}/courses/{courseId:long}")]
        [Authorize(Roles = "Administrator, Tutor")]
        public async Task<IActionResult> AddCourset(long testId, long courseId)
        {
            if (!await _repository.Exists(testId)) return BadRequest("Such test does not exist");

            return Ok(await (_repository as ITestRepository).AddCourse(testId, courseId));
        }

        /// <summary>
        /// \~english Removes a test from a course
        /// \~ukrainian Видаляє тест з курсу
        /// </summary>
        /// <param name="testId">
        /// \~english Test id
        /// \~ukrainian Id тесту
        /// </param>
        /// <param name="courseId">
        /// \~english Course id
        /// \~ukrainian Id курсу
        /// </param>
        /// <returns>
        /// \~english a test-course connection
        /// \~ukrainian зв'язок між тестом та курсом
        /// </returns>
        /// <example>
        /// \~english An expample of HTTP request
        /// \~ukrainian Приклад HTTP запиту
        /// <code>
        /// DELETE: api/tests/5/courses/2
        /// </code>
        /// </example>
        [HttpDelete("{testId:long}/courses/{subjectId:long}")]
        [Authorize(Roles = "Administrator, Tutor")]
        public async Task<IActionResult> RemoveCourse(long testId, long courseId)
        {
            if (!await _repository.Exists(testId)) return BadRequest("Such test does not exist");
            var deleted = await (_repository as ITestRepository).RemoveCourse(testId, courseId);
            if (!deleted.Deleted) return BadRequest("The test does not belong to such course");
            return Ok(deleted);
        }
    }
}