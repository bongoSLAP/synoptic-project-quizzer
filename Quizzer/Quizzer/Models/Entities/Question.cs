using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quizzer.Models.Bases;

namespace Quizzer.Models.Entities;

public class Question : IdBase
{
    [Column(TypeName = "nvarchar(512)")]
    [Required(ErrorMessage = "Question Text field is required.")]
    public string Text { get; set; } = string.Empty;
    [Column(TypeName = "int")]
    [Required(ErrorMessage = "Index field is required.")]
    public int Index { get; set; }
    public Guid QuizId { get; set; } = Guid.Empty;
    public virtual Quiz? Quiz { get; set; }
    public virtual ICollection<Answer>? Answers { get; set; }
}
    