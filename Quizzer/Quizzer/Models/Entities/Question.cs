using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Quizzer.Models.Bases;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Models.Entities;

public class Question : IdBase
{
    [Column(TypeName = "nvarchar(512)")]
    [Required(ErrorMessage = "Question Text field is required.")]
    public string Text { get; set; } = string.Empty;
    [Column(TypeName = "int")]
    [Required(ErrorMessage = "Answer Index field is required.")]
    public int QuestionIndex { get; set; }
    public Guid QuizId { get; set; } = Guid.Empty;
    public virtual Quiz? Quiz { get; set; }
    public virtual ICollection<Answer>? Answers { get; set; }

    public QuestionInfo Map()
    {
        return new QuestionInfo()
        {
            Text = Text,
            QuestionIndex = QuestionIndex,
            Answers = Answers?.Select(a => a.Map()).ToList()
        };
    }
}
    