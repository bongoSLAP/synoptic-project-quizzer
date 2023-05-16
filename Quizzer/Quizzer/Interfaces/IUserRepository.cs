using System.Security.Claims;
using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IUserRepository
{
    User? GetByUsername(string? username);
    void Add(User user);
}