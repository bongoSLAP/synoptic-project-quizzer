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
    
    private Claim? GetUsernameClaim(ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier);
    }

    public User? GetByUsername(string? username)
    {
        return _db.User.FirstOrDefault(u =>
            username != null && u.Username != null && u.Username.ToLower() == username.ToLower()
        );
    }
    
    public User? GetByClaim(ClaimsPrincipal user)
    {
        var usernameClaim = GetUsernameClaim(user);
        return GetByUsername(usernameClaim?.Value);
    }
    
    public void Add(User user)
    {
        _db.User.Add(user);
        _db.SaveChanges();
    }
}