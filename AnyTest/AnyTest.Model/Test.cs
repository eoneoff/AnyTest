using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterName))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Name))]
        public string Name { get; set; }
        public long AuthorId { get; set; }
        public bool Changed { get; set; }

        [Timestamp]
        public byte[] RowVerstion { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public virtual Tutor Author { get; set; }
        public virtual ICollection<TutorTest> Owners { get; set; }
        public virtual ICollection<StudentTest> Students { get; set; }
        public virtual ICollection<TestSubject> Subjects { get; set; }
        public virtual ICollection<TestCourse> Courses { get; set; }
        public virtual ICollection<TestPass> Passes { get; set; }
    }
}
