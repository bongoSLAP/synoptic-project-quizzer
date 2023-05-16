using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quizzer.Models.Bases;

namespace Quizzer.Models.Entities;

public class Answer : IdBase
{
    [Column(TypeName = "nvarchar(512)")]
    [Required(ErrorMessage = "Answer Text field is required.")]
    public string Text { get; set; } = string.Empty;
    [Column(TypeName = "int")]
    [Required(ErrorMessage = "Index field is required.")]
    public int Index { get; set; } 
    public Guid QuestionId { get; set; } = Guid.Empty;
    public virtual Question? Question { get; set; } 
}