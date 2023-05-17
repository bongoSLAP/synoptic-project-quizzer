using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzer.Interfaces;

namespace Quizzer.Controllers;

public class QuizController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IQuizHandler _quizHandler;
    public QuizController(IUserRepository userRepository, IQuizHandler quizHandler)
    {
        _userRepository = userRepository;
        _quizHandler = quizHandler;
    }

    [Authorize]
    [HttpGet("Quiz/List")]
    public IActionResult ListAll()
    {
        var user = _userRepository.GetByClaim(HttpContext.User);

        if (user == null)
            return Unauthorized("User unrecognised.");

        try
        {
            var quizzes = _quizHandler.List(user.Role);
            return Ok(quizzes);
        }
        catch (InvalidOperationException)
        {
            return NoContent();
        }
    }
}