using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.Model
{
    public class TestCourse
    {
        public ulong TestId { get; set; }
        public ulong CourseId { get; set; }
        public uint OrderNo { get; set; }

        public virtual ICollection<Precondition> Preconditions { get; set; }
        public virtual ICollection<Precondition> Dependent { get; set; }
        //TODO: multiple key, bind to precondition TestId, dependent preconditionId
    }
}
