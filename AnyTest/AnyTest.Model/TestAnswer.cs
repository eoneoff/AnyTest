﻿using AnyTest.ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.Model
{
    public class TestAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = nameof(Resources.EnterAnswer))]
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.Answer))]
        public string Answer { get; set; }

        [Range(0, 100)]
        public int Percent { get; set; }

        [Required]
        public int OrderNo { get; set; }

        [Required]
        public long TestQuestionId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(TestQuestionId))]
        public virtual TestQuestion TestQuestion { get; set; }
    }
}
