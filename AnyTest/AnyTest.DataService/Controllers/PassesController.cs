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
    ///<inheritdoc/>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tutor")]
    public class PassesController : ApiControllerBase<TestPass>
    {
        ///<inheritdoc/>
        public PassesController(IRepository<TestPass> repository) : base(repository)
        {
        }

    }
}