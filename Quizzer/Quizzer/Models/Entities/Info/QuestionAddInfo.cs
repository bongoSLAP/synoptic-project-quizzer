namespace Quizzer.Models.Entities.Info;

public class QuestionAddInfo
{
    public QuestionInfo QuestionInfo { get; set; } = new QuestionInfo();
    public Guid QuizId { get; set; } = Guid.Empty;
}