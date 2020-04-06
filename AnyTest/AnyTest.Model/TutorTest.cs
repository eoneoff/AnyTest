using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class TutorTest
    {
        public long TutorId { get; set; }
        public long TestId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual Test Test { get; set; }
    }
}
