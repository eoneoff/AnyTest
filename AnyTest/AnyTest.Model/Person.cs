using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AnyTest.ResourceLibrary;

namespace AnyTest.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterFirstName))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.FirstName))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterFamilyName))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.FamilyName))]
        public string FamilyName { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Patronimic))]
        public string Partronimig { get; set; }

        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Phone))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterEmail))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EmailInvalid))]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual Student Student { get; set; }
    }
}
