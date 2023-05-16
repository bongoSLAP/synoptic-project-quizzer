using Quizzer.Models.Bases;
using Quizzer.Models.Entities;

namespace Quizzer.Interfaces;

public interface ILoginHandler
{
    User? Authenticate(UserLogin userLogin);
    string Generate(User user);
}