using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.LoginRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UserName))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterEmail))]
        [EmailAddress]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordLengthWarning), MinimumLength = 6)]
        [ContainsLoweCase(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainLowerCase))]
        [ContainsUpperCase(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainUppercase))]
        [ContainsDigit(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainDigits))]
        [ContainsSymbol(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordMustContainSymbols))]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Password))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.ConfirmaitonNotMahch))]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }

    public class ContainsLoweCase : RegularExpressionAttribute
    {
        public ContainsLoweCase() : base("[a-z]") { }
    }

    public class ContainsUpperCase : RegularExpressionAttribute
    {
        public ContainsUpperCase() : base ("[A-Z]") { }
    }

    public class ContainsDigit : RegularExpressionAttribute
    {
        public ContainsDigit() : base(@"\d") {}
    }

    public class ContainsSymbol : RegularExpressionAttribute
    {
        public ContainsSymbol() : base(@"[^a-z|^A-Z|^\d]") { }
    }
}
