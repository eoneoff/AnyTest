using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnyTest.ClientAuthentication;
using Microsoft.AspNetCore.Identity;
using AnyTest.IDataRepository;
using AnyTest.Model;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnyTest.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPersonRepository _people;

        public AccountsController(UserManager<IdentityUser> userManager, IPersonRepository people)
        {
            _userManager = userManager;
            _people = people;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser { UserName = model.UserName, Email = model.Email };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            if(AuthService.Roles.Contains(model.Role))
            {
                await _userManager.AddToRoleAsync(newUser, model.Role);
            }

            return Ok(new RegisterResult { Successful = true });
        }

        [HttpGet("person")]
        [Authorize(Roles = "Administrator")]
        public async Task<Person> Person ()
        {
            Person person = new Person();

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var email = identity.FindFirst(ClaimTypes.Email).Value;                
                person = await _people.FindByEmail(email);
            }

            return person;
        }
    }
}
