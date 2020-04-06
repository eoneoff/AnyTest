using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.Name))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Name))]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
