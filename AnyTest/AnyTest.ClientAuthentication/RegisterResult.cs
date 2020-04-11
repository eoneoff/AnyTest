using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A class for a result of a user registration
    /// \~ukrainian Клас для реєстрації результатов реєстрації нового користувача
    /// </summary>
    public class RegisterResult
    {
        /// <summary>
        /// \~english Defines a successfull registration
        /// \~ukrainian Позначає результат реєстрації
        /// </summary>
        public bool Successful { get; set; }

        /// <summary>
        /// \~english Contains registration errors
        /// \~ukrainian Містить помилки реєстрації
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
