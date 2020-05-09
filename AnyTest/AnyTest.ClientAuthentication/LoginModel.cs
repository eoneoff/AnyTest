using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A class for user login model
    /// \~ukrainian Клас моделі входа користувача 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// \~english User name
        /// \~ukrainian Ім'я користувача
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.LoginRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UserName))]
        public virtual string UserName { get; set; }

        /// <summary>
        /// \~english Password
        /// \~ukrainian Пароль
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Password))]
        public virtual string Password { get; set; }

        /// <summary>
        /// \~english Parameter to remember user
        /// \~ukrainian Запам'ятати користувача
        /// </summary>
        public virtual bool RememberMe { get; set; }
    }
}
