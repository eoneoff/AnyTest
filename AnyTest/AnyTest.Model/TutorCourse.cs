using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class TutorCourse
    {
        public ulong TutorId { get; set; }
        public ulong CourseId { get; set; }

        [ForeignKey(nameof(TutorId))]
        public virtual Tutor Tutor { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }

        //TODO multiple key
    }
}
