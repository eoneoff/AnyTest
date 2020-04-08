using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    public class LoginResult
    {
        public bool Sussessfull { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
