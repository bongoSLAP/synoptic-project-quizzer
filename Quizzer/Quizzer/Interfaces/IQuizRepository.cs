using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IQuizRepository
{
    Quiz? GetByQuestionId(Guid id);
    Quiz GetById(Guid id);
    IEnumerable<Quiz> List();
    IEnumerable<Quiz> ListIncludeAnswers();
}