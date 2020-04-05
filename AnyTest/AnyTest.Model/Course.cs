using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterName))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Name))]
        public string Name { get; set; }
        public ulong? SubjectId { get; set; }
        public ulong AuthorId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject Subject { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Tutor Author { get; set; }
         
        public virtual ICollection<TutorCourse> Owners { get; set; }
    }
}
