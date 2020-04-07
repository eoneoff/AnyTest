using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnyTest.DataService.Controllers
{
    [Route("/")]
    public class StartupController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Server Up";
        }
    }
}