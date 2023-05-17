namespace Quizzer.Models.Entities.Info;

public class QuizInfo
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<QuestionInfo>? Questions { get; set; } 
}