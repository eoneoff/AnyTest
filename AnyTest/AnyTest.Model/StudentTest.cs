using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class StudentTest
    {
        public long StudentId { get; set; }
        public long TestId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Test Test { get; set; }
    }
}
