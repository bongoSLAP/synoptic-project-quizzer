using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzer.Interfaces;
using Quizzer.Models.Bases;

namespace Quizzer.Controllers;

public class LoginController : Controller
{
    private readonly ILoginHandler _loginHandler; 

    public LoginController(ILoginHandler loginHandler)
    {
        _loginHandler = loginHandler;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        try
        {
            var user = _loginHandler.Authenticate(userLogin);
            if (user != null)
            {
                var token = _loginHandler.Generate(user);
                return Ok(token);
            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}