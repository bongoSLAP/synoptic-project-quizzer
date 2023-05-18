namespace Quizzer.Models.Entities.Info;

public class QuestionInfo
{
    public string Text { get; set; } = string.Empty;
    public int QuestionIndex { get; set; }
    public ICollection<AnswerInfo>? Answers { get; set; }
}