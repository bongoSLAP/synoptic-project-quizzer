using System.Security.Claims;
using Quizzer.Data;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;

namespace Quizzer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly QuizzerContext _db;

    public UserRepository(QuizzerContext db)
    {
        _db = db;
    }
    
    public User? GetByUsername(string? username)
    {
        return _db.User.FirstOrDefault(u =>
            username != null && u.Username != null && u.Username.ToLower() == username.ToLower()
        );
    }
}