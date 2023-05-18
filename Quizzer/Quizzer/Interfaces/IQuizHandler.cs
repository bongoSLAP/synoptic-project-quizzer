using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;

namespace Quizzer.Interfaces;

public interface IQuizHandler
{
    IEnumerable<QuizInfo> List(Role role);
}