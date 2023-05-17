using System.Security.Claims;
using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IUserRepository
{
    User? GetByUsername(string? username);
    User? GetByClaim(ClaimsPrincipal user);
    void Add(User user);
}