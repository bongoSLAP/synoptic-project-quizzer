﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quizzer.Models.Bases;

namespace Quizzer.Models.Entities;

public class Quiz : IdBase
{
    [Column(TypeName = "nvarchar(120)")]
    [Required(ErrorMessage = "Title field is required.")]
    public string Title { get; set; } = string.Empty;
    [Column(TypeName = "nvarchar(255)")]
    public string? Description { get; set; }
    public virtual ICollection<Question>? Questions { get; set; } 
}