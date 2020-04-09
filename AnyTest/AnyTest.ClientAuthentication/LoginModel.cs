using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnyTest.ClientAuthentication
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.LoginRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UserName))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.PasswordRequired))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Password))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
