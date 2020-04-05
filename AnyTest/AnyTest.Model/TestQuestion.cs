using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnyTest.Model
{
    public class TestQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public ulong Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterQuestion))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Question))]
        public string Question { get; set; }

        [Required]
        public ulong TestId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual Test Test { get; set; }
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
