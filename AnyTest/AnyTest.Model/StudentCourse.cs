using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnyTest.Model
{
    public class StudentCourse
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }
}
