using System.Security.Claims;
using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface IUserRepository
{
    public User? GetByUsername(string? username);
}