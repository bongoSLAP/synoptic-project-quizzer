using Microsoft.EntityFrameworkCore;
using Quizzer.Data;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;

namespace Quizzer.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly QuizzerContext _db;
    
    public QuizRepository(QuizzerContext db)
    {
        _db = db;
    }
    
    public Quiz? GetByQuestionId(Guid id)
    {
        return _db.Quiz.FirstOrDefault(qz => qz.Questions != null && qz.Questions.Any(q => q.Id == id));
    }

    public Quiz GetById(Guid id)
    {
        var quiz = _db.Quiz.Find(id);

        if (quiz == null)
            throw new InvalidOperationException("Quiz does not exist.");

        return quiz;
    }
    
    public IEnumerable<Quiz> List()
    {
        return _db.Quiz
            .Include(qz => qz.Questions)
            .AsEnumerable();
    }

    public IEnumerable<Quiz> ListIncludeAnswers()
    {
        return _db.Quiz
            .Include(q => q.Questions)!
            .ThenInclude(q => q.Answers)
            .AsEnumerable(); 
    }
}