using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english An inteface for JWT authentication service
    /// \~ukrainian Ітерфейс для служби JWT аутентифікації
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// \~english Performs user login
        /// \~ukrainian Виконує вхід користувача
        /// </summary>
        /// <param name="creds">
        /// \~english <c>LoginModel</c> object with user credentials
        /// \~ukrainian Об'єкт класу <c>LoginModel</c> із даними користувача 
        /// </param>
        /// <returns><c>LoginResult</c> object with JWT token or error info</returns>
        Task<LoginResult> Login(LoginModel creds);

        /// <summary>
        /// \~english Performs user logout
        /// \~ukrainian Виконує вихід користувача
        /// </summary>
        /// <returns></returns>
        Task Logout();

        /// <summary>
        /// \~english Registers a new user
        /// \~ukrainian реєструє нового користувача
        /// </summary>
        /// <param name="registerModel">
        /// \~english A <c>RegisterModel</c> object with new user data
        /// \~ukrainian Об'єкт класу <c>RegisterModel</c> з даними нового користувача
        /// </param>
        /// <returns>
        /// \~english <c>RegisterResult</c> object with info about registration
        /// \~ukrainian Об'єкт класу <c>RegisterResult</c> з інформацією про реєстрацію
        /// </returns>
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
