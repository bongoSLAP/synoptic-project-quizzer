using Quizzer.Models.Entities.Info;

namespace Quizzer.Interfaces;

public interface IAnswerHandler
{
    void Add(AnswerInfo answerInfo, Guid questionId);
    void Delete(Guid answerId);
    void Edit(AnswerInfo answerInfo);
}