using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class TestSubject
    {
        public long TestId { get; set; }
        public long SubjectId { get; set; }
        public bool Deleted { get; set; }
        [JsonIgnore]
        public virtual Test Test { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }
    }
}
