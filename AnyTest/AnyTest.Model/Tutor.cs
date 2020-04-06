using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Tutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual Person Person { get; set; }

        public virtual ICollection<Test> TestsCreated { get; set; }
        public virtual ICollection<Course> CoursesCreated { get; set; }
        public virtual ICollection<TutorTest> TestsOwned { get; set; }
        public virtual ICollection<TutorCourse> CoursesOwned { get; set; }
    }
}
