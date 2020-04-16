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
        [ContainsLoweCase(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainLowerCase))]
        [ContainsUpperCase(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainUppercase))]
        [ContainsDigit(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainDigits))]
        [ContainsSymbol(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainSymbols))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Password))]
        public string Password { get; set; }

        /// <summary>
        /// \~english Password confirmation
        /// \~ukrainian Підтвердження паролю
        /// </summary>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ConfirmPassword))]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.ConfirmaitonNotMahch))]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// \~english A role for a new user
        /// \~ukrainian Роль нового користувача
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();
    }

    /// <summary>
    /// \~english  Specifies that a data field value in ASP.NET Dynamic Data must contain lowercase letters
    /// \~ukrainian Визначає, що поле даних для ASP.NET Dynamic Data має містити строчні літери
    /// </summary>
    public class ContainsLoweCaseAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// \~english Initializes a new instance of <c>ContainsLowerCase</c> class
        /// \~ukrainian Ініціалізує новий екземлпяр класу <c>ContainsLowerCase</c>
        /// </summary>
        public ContainsLoweCaseAttribute() : base(".*[a-z]+.*") { }
    }

    /// <summary>
    /// \~english  Specifies that a data field value in ASP.NET Dynamic Data must contain lowercase letters
    /// \~ukrainian Визначає, що поле даних для ASP.NET Dynamic Data має містити заглавні літери
    /// </summary>
    public class ContainsUpperCaseAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// \~english Initializes a new instance of <c>ContainsUpperCase</c> class
        /// \~ukrainian Ініціалізує новий екземлпяр класу <c>ContainsUpperCase</c>
        /// </summary>
        public ContainsUpperCaseAttribute() : base (".*[A-Z]+.*") { }
    }

    /// <summary>
    /// \~english  Specifies that a data field value in ASP.NET Dynamic Data must contain a digin
    /// \~ukrainian Визначає, що поле даних для ASP.NET Dynamic Data має містити цифри
    /// </summary>
    public class ContainsDigitAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// \~english Initializes a new instance of <c>ContainsDigit</c> class
        /// \~ukrainian Ініціалізує новий екземлпяр класу <c>ContainsDigit</c>
        /// </summary>
        public ContainsDigitAttribute() : base(@".*\d+.*") {}
    }

    /// <summary>
    /// \~english  Specifies that a data field value in ASP.NET Dynamic Data must contain a special symbol
    /// \~ukrainian Визначає, що поле даних для ASP.NET Dynamic Data має містити символи
    /// </summary>
    public class ContainsSymbolAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// \~english Initializes a new instance of <c>ContainsSymbol</c> class
        /// \~ukrainian Ініціалізує новий екземлпяр класу <c>ContainsLowerSymbol</c>
        /// </summary>
        public ContainsSymbolAttribute() : base(@".*[^a-z|^A-Z|^\d]+.*") { }
    }
}
