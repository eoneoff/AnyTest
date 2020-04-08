using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.ClientAuthentication
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel creds);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
