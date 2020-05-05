using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        public virtual TestPass Pass { get; set; }

        public virtual TestQuestion Question { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public virtual TestAnswer Answer { get; set; }
    }
}
