using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [ForeignKey(nameof(Id))]
        public virtual Person Person { get; set; }

        public virtual ICollection<StudentTest> Tests { get; set; }
        public virtual ICollection<StudentCourse> Courses { get; set; }
    }
}
