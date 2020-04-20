using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.Model
{
    public class TestSubject
    {
        public long TestId { get; set; }
        public long SubjectId { get; set; }
        public virtual Test Test { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
