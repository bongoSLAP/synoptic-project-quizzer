namespace Quizzer.Models.Entities.Info;

public class AnswerInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public int AnswerIndex { get; set; } 
}