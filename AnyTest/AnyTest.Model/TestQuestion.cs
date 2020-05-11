using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class TestQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterQuestion))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Question))]
        public virtual string Question { get; set; }

        [Required]
        public long TestId { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
        public virtual ICollection<AnswerPass> Answered { get; set; }
    }
}
