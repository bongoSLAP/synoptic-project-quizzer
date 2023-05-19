using Quizzer.Data;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;

namespace Quizzer.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly QuizzerContext _db;

    public QuestionRepository(QuizzerContext context)
    {
        _db = context;
    }

    public Question GetById(Guid id)
    {
        var question = _db.Question.Find(id);
        if (question == null) 
            throw new InvalidOperationException("Question does not exist.");

        return question;
    }

    public void Add(Question question)
    {
        var existingQuestion = _db.Question.Find(question.Id);
        if (existingQuestion != null)
            throw new InvalidOperationException("Question already exists.");

        _db.Question.Add(question);
        _db.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var question = GetById(id);
        
        _db.Question.Remove(question);
        _db.SaveChanges();
    }

    public void Upsert(Question question)
    {
        var existingQuestion = _db.Question.Find(question.Id);
        if (existingQuestion != null)
        {
            _db.Entry(existingQuestion).CurrentValues.SetValues(question);
            _db.SaveChanges();
        }
        else
        {
            _db.Question.Add(question);
            _db.SaveChanges();
        }
    }
    
    public void ReindexQuestionNumbers(Guid quizId)
    {
        var questions = _db.Question.Where(q => q.QuizId == quizId).OrderBy(q => q.QuestionIndex).ToList();
        for (var i = 0; i < questions.Count; i++)
        {
            questions[i].QuestionIndex = i ;
        }

        _db.SaveChanges();
    }
}