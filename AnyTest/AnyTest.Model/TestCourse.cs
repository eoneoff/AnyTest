using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        public virtual ICollection<Precondition> Preconditions { get; set; }
        public virtual ICollection<Precondition> Dependent { get; set; }
    }
}
