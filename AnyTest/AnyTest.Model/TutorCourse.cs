﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class TutorCourse
    {
        public long TutorId { get; set; }
        public long CourseId { get; set; }

        public virtual Tutor Tutor { get; set; }
        public virtual Course Course { get; set; }
    }
}
