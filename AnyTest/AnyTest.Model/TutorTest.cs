using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class TutorTest
    {
        public ulong TutorId { get; set; }
        public ulong TestId { get; set; }

        [ForeignKey(nameof(TutorId))]
        public virtual Tutor Tutor { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }

        //TODO multiple key
    }
}
