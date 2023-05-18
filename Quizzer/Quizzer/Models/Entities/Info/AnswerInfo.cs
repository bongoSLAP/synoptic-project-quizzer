namespace Quizzer.Models.Entities.Info;

public class AnswerInfo
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Text { get; set; } = string.Empty;
    public int AnswerIndex { get; set; } 
    public bool IsCorrect { get; set; }
}