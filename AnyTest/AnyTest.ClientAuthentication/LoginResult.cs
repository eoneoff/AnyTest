using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A model of a login result
    /// \~ukrainian Модель результату входу користувача
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// \~english Identifies succefull login
        /// \~ukrainian Позначає успішний вхід
        /// </summary>
        public bool Sussessfull { get; set; }

        /// <summary>
        /// \~english Contains data about login errors
        /// \~ukrainian Містить дані про помилки входу
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// \~english A JWT token for a user
        /// \~ukrainian JWT токен корисувача
        /// </summary>
        public string Token { get; set; }
    }
}
