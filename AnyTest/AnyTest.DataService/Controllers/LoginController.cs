using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AnyTest.ClientAuthentication;
using AnyTest.ResourceLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnyTest.DataService.Controllers
{
    /// <summary>
    /// \~english An API controller for user login
    /// \~ukrainian Контроллер API для входу користувача
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// \~english Creates new instance of <c>LoginController</c>
        /// \~ukrainian Створює новий екземляр <c>LoginController</c>
        /// </summary>
        ///  /// <param name="configuration">
        /// \~english The class <c>IConfiguration</c> instance. Dependency.
        /// \~ukrainian Екземпляр класу, який втілює <c>IConfiguration</c>. Залежність
        /// </param>
        /// /// <param name="signInManager">
        /// \~english <c>SignInManager{IdentitityUser}</c> instance. Dependency.
        /// \~ukrainian Екземпляр <c>SignInManager{IdentitityUser}</c>. Залежність.
        /// </param>
        /// <param name="userManager">
        /// \~english <c>UserManager{IdentitityUser}</c> instance. Dependency.
        /// \~ukrainian Екземпляр <c>UserManager{IdentitityUser}</c>. Залежність.
        /// </param>
        public LoginController(IConfiguration configuration, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

         /// <summary>
        /// \~english Logs into application
        /// \~ukrainian Вхід в додаток
        /// </summary>
        /// <param name="login">
        /// \~english An instance of a <c>LoginrModel</c>, conatining new user acccount data
        /// \~ukrainian Екземпляр класу <c>LoginModel</c>, якій місить дані нового користувача
        /// </param>
        /// <returns>
        /// \~english A result of a user login
        /// \~ukrainian Результат входу в додаток
        /// </returns>
        /// <example>
        /// \~english An example of HTTP request to for login
        /// \~ukrainian Приклад HTTP запиту входу в додаток
        /// <code>
        /// POST: api/Login
        /// </code>
        /// </example>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if(!result.Succeeded)
            {
                return BadRequest(new LoginResult { Sussessfull = false, Error = Resources.UsernameOrPasswordInvalid });
            }

            var user = await _userManager.FindByNameAsync(login.UserName);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "Token");
            identity.AddClaims(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtSettings:JwtExpiryDays"]));

            var token = new JwtSecurityToken (null, null, identity.Claims, expires:expiry, signingCredentials: creds);

            return Ok(new LoginResult { Sussessfull = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
