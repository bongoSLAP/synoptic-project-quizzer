using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Quizzer.Controllers;

public class QuizController : Controller
{
    public QuizController()
    {
        
    }

    [Authorize]
    [HttpGet("Quiz/List")]
    public IActionResult ListAll()
    {
        
    }
}