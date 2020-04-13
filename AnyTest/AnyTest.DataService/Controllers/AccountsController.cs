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
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnyTest.DataService.Controllers
{
    /// <summary>
    /// \~english A controller for managing user accounts
    /// \~ukrainia Контроллер для керуввання аккаунтами користувачі
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AnyTestIdentityDbContext _idb;
        private readonly IPersonRepository _people;

        /// <summary>
        /// \~english Creates new instance of <c>AccountsController</c>
        /// \~ukrainian Створює новий екземляр <c>AccountsController</c>
        /// </summary>
        /// <param name="userManager">
        /// \~english <c>UserManager{IdentitityUser}</c> instance. Dependency.
        /// \~ukrainian Екземпляр <c>UserManager{IdentitityUser}</c>. Залежність.
        /// </param>
        /// <param name="people">
        /// \~english The class <c>IPersonRepository</c> instance. Dependency.
        /// \~ukrainian Екземпляр класу, який втілює <c>IPersonRepository</c>. Залежність
        /// </param>
        public AccountsController(UserManager<IdentityUser> userManager, IPersonRepository people, AnyTestIdentityDbContext idb)
        {
            _userManager = userManager;
            _people = people;
            _idb = idb;
        }

        /// <summary>
        /// \~english Creates a new user account
        /// \~ukrainian Створює нового користувача
        /// </summary>
        /// <param name="model">
        /// \~english An instance of a <c>RegisterModel</c>, conatining new user acccount data
        /// \~ukrainian Екземпляр класу <c>RegisterModel</c>, якій місить дані нового користувача
        /// </param>
        /// <returns>
        /// \~english A result of an account creation
        /// \~ukrainian Результат створення нового користувача
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to create a person
        /// \~ukrainian Приклад HTTP запиту створення особистих даних користувача
        /// <code>
        /// POST: api/Accounts
        /// </code>
        /// </example>
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


        /// <summary>
        /// \~english Returns a <c>Person</c> correspoinding current registered user
        /// \~ukrainian Повертає особисті дані разреєстрованого користувача
        /// </summary>
        /// <returns>
        /// \~english A <c>Person</c> object with user data
        /// \~ukrainian Об'єкт класу <c>Person</c> з особистими даними користувача
        /// </returns>
        /// /// <example>
        /// \~english An example of HTTP request to get registered user data
        /// \~ukrainian Приклад HTTP запиту особистих даних зареєстрованого користувача
        /// <code>
        /// GET: api/Accounts/Person
        /// </code>
        /// </example>
        [HttpGet("person")]
        [Authorize]
        public async Task<Person> Person ()
        {
            Person person = new Person();

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var email = identity.FindFirst(ClaimTypes.Email).Value;                
                person = await _people.FindByEmail(email) ?? new Person { Email = email};
            }

            return person; 
        }

        /// <summary>
        /// \~english Returns an info about all registered users
        /// \~ukrainian Пвертає дані усіх зареєстрованих користувачів
        /// </summary>
        /// <returns>
        /// \~english A collection of user info
        /// \~ukrainian Колекцію даних всіх зареєстрованих користувачів
        /// </returns>
        /// /// <example>
        /// \~english An example of HTTP request to get all registered user's info
        /// \~ukrainian Приклад HTTP запиту даних усіх зареєстроваих користувачів
        /// <code>
        /// GET: api/Accounts/Users
        /// </code>
        /// </example>
        [HttpGet("users")]
        [Authorize(Roles = "Administrator")]
        public async Task<IEnumerable<UserInfo>> Users()
        {
            var identityUserInfo = await _idb.UserInfos();
            var personalInfo = await _people.Get();

            var result = from user in identityUserInfo
                     join person in personalInfo on user.Email equals person.Email into grp
                     from pordef in grp.DefaultIfEmpty()
                     select new UserInfo
                     {
                         User = user.User,
                         Email = user.Email,
                         Name = $"{pordef?.FamilyName ?? ""} {pordef?.FirstName ?? ""} {pordef?.Patronimic ?? ""}".Trim(),
                         UserPersonId = pordef?.Id,
                         Roles = user.Roles
                     };

            return result;
        }
    }
}
