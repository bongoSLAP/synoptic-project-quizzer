using Quizzer.Models.Entities;
using Quizzer.Models.Enums;

namespace Quizzer.Interfaces;

public interface IQuizHandler
{
    IEnumerable<Quiz> List(Role role);
}