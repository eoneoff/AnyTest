using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnyTest.DataService.Controllers
{
    ///<inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ApiControllerBase<Course>
    {
        ///<inheritdoc />
        public CoursesController(IRepository<Course> repository) : base(repository) { }

        ///<inheritdoc />
        [Authorize(Roles = "Administrator, Tutor")]
        public override async Task<IActionResult> Post(Course item) => await base.Post(item);

        ///<inheritdoc />
        [Authorize(Roles = "Administrator, Tutor")]
        public override async Task<IActionResult> Put(long id, Course item) => await base.Put(id, item);

        ///<inheritdoc />
        [Authorize(Roles = "Administrator, Tutor")]
        public override async Task<IActionResult> Delete(long id) => await base.Delete(id);
    }
}