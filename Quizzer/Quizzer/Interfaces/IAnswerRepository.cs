using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IAnswerRepository
{
    Answer GetById(Guid id);
    void Add(Answer answer);
    void Delete(Guid id);
    void Upsert(Answer answer);
    void ReindexAnswerNumbers(Guid questionId);
}