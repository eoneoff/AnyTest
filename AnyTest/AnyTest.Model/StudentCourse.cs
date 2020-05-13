using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class StudentCourse
    {
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public bool Removed { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }

        [JsonIgnore]
        public virtual Course Course { get; set; }
    }
}
