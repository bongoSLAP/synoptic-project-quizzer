using Quizzer.Data;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;

namespace Quizzer.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly QuizzerContext _db;

    public AnswerRepository(QuizzerContext context)
    {
        _db = context;
    }
    
    public Answer GetById(Guid id)
    {
        var answer = _db.Answer.Find(id);
        if (answer == null) 
            throw new InvalidOperationException("Answer does not exist.");

        return answer;
    }

    public void Add(Answer answer)
    {
        var existingAnswer = _db.Question.Find(answer.Id);
        if (existingAnswer != null)
            throw new InvalidOperationException("Answer already exists.");

        _db.Answer.Add(answer);
        _db.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var answer = GetById(id);
        
        _db.Answer.Remove(answer);
        _db.SaveChanges();
    }

    public void Upsert(Answer answer)
    {
        var existingQuestion = _db.Answer.Find(answer.Id);
        if (existingQuestion != null)
        {
            _db.Entry(existingQuestion).CurrentValues.SetValues(answer);
            _db.SaveChanges();
        }
        else
        {
            _db.Answer.Add(answer);
            _db.SaveChanges();
        }
    }
    
    public void ReindexAnswerNumbers(Guid questionId)
    {
        var answers = _db.Answer.Where(a => a.QuestionId == questionId).OrderBy(a => a.AnswerIndex).ToList();
        for (var i = 0; i < answers.Count; i++)
        {
            answers[i].AnswerIndex = i;
        }

        _db.SaveChanges();
    }
}