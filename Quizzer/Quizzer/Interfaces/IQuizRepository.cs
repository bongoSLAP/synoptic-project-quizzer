using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IQuizRepository
{
    IEnumerable<Quiz> List();
    IEnumerable<Quiz> ListIncludeAnswers();
}