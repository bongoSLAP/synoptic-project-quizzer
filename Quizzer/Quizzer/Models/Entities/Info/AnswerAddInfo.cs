namespace Quizzer.Models.Entities.Info;

public class AnswerAddInfo
{
    public AnswerInfo AnswerInfo { get; set; } = new AnswerInfo();
    public Guid QuestionId { get; set; }
}