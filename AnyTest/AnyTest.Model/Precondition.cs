using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Precondition
    {
        public long TestId { get; set; }
        public long PreconditionId { get; set; }

        public virtual TestCourse Test { get; set; }
        public virtual TestCourse PreconditionTest { get; set; }
    }
}
