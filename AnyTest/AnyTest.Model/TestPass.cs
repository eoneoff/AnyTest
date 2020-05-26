using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class TestPass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long TestId { get; set; }

        [Required]
        public long StudentId { get; set; }

        [Required]
        public DateTime PassedAt { get; set; }

        [JsonIgnore]
        public virtual Test Test { get; set; }
        public virtual ICollection<AnswerPass> Answers { get; set; }
    }
}
