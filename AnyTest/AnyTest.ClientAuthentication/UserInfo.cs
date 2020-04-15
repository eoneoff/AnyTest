using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A class, containing user info
    /// \~ukrainian Клас, якій містить дані користувача
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// \~english Identity user name
        /// \~ukrainian Логін користувач
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// \~english User email
        /// \~ukrainian Email користувача
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// \~english A collection of user's roles
        /// \~ukrainian Колекція ролей користувача
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// \~english Id of users's <c>Person</c>
        /// \~ukrainian Id особистих даних користувача
        /// </summary>
        public long? UserPersonId { get; set; }

        /// <summary>
        /// \~english Usert's full name
        /// \~ukrainian Повне ім'я користувача
        /// </summary>
        public string Name { get; set; }
    }
}
