using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class TestCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long TestId { get; set; }
        public long CourseId { get; set; }
        public uint OrderNo { get; set; }
        public bool Deleted { get; set; }
        public bool Restricted { get; set; }
        [Range(0, 100)]
        public int PassScore { get; set; } = 50;
        public int TriesPermitted { get; set; }

        [JsonIgnore]
        public virtual Test Test { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual ICollection<Precondition> Preconditions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Precondition> Dependent { get; set; }
    }
}
