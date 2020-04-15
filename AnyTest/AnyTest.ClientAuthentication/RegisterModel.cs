using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    /// <summary>
    /// \~english A model for user registering
    /// \~ukrainian Модель для реєстрації корисувача
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// \~english A desired user name
        /// \~ukrainian Бажане ім'я користувача
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.LoginRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UserName))]
        public string UserName { get; set; }

        /// <summary>
        /// \~english An email for a new user
        /// \~ukrainian Email нового користувача
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterEmail))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.WrongEmailFormat))]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        /// <summary>
        /// \~english New user password
        /// \~ukrainian Пароль нового користувача
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordRequired))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordLengthWarning), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Password))]
        public string Password { get; set; }

        /// <summary>
        /// \~english Password confirmation
        /// \~ukrainian Підтвердження паролю
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.ConfirmaitonNotMahch))]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// \~english A role for a new user
        /// \~ukrainian Роль нового користувача
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}
