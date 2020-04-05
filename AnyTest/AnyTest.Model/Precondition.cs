using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class Precondition
    {
        public uint TestId { get; set; }
        public uint PreconditionId { get; set; }

        public virtual TestCourse Test { get; set; }
        public virtual TestCourse PreconditionTest { get; set; }
    }
}
