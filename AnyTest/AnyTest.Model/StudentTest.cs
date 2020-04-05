using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class StudentTest
    {
        public ulong StudentId { get; set; }
        public ulong TestId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }

        //TODO multiple key
    }
}
