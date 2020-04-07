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
    [Route("api/[controller]")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private IRepository<Tutor> _repository;

        public TutorsController(IRepository<Tutor> repository) => _repository = repository;

        [HttpGet]
        public async Task<IEnumerable<Tutor>> Get() => await _repository.Get();

        [HttpGet("{id:long}")]
        public async Task<Tutor> Get(long id) => await _repository.Get(id);
    }
}