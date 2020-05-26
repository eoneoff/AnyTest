using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class AnswerPass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long PassId { get; set; }

        [Required]
        public long QuestionId { get; set; }

        [Required]
        public long AnswerId { get; set; }

        [JsonIgnore]
        public virtual TestPass Pass { get; set; }

        [JsonIgnore]
        public virtual TestQuestion Question { get; set; }

        [ForeignKey(nameof(AnswerId))]
        [JsonIgnore]
        public virtual TestAnswer Answer { get; set; }
    }
}
