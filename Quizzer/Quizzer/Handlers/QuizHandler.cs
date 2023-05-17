using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Enums;

namespace Quizzer.Handlers;

public class QuizHandler : IQuizHandler
{
    private readonly IQuizRepository _quizRepository;
    
    public QuizHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }
    
    public IEnumerable<Quiz> List(Role role)
    {
        var quizzes = role == Role.Restricted ? _quizRepository.List() : _quizRepository.ListIncludeAnswers();
        
        if (!quizzes.Any())
            throw new InvalidOperationException("No quizzes found");

        return quizzes;
    }
}