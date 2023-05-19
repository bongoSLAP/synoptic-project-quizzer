using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Interfaces;

public interface IQuestionHandler
{
    void Add(QuestionInfo question, Guid quizId);
    void Delete(Guid questionId);
    void Edit(QuestionInfo questionInfo);
}