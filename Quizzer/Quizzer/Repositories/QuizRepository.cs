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