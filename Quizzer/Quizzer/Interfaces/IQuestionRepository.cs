using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IQuestionRepository
{
    public Question GetById(Guid id);
    void Add(Question question);
    void Delete(Guid questionId);
    void Upsert(Question question);
    public void ReindexQuestionNumbers(Guid quizId);
}