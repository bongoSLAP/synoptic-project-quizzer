using Microsoft.AspNetCore.Mvc;
using Quizzer.Data;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Enums;

namespace Quizzer.Controllers;

public class SignupController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IScryptEncoder _encoder;
    private readonly QuizzerContext _db;

    public SignupController(IUserRepository userRepository, IScryptEncoder encoder, QuizzerContext db)
    {
        _userRepository = userRepository;
        _encoder = encoder;
        _db = db;
    }

    [HttpPost("Signup")]
    public IActionResult Create([FromBody] User? user)
    {
        if (user == null)
            return BadRequest("User is null");
            
        try
        {
            User? checkExists = _userRepository.GetByUsername(user.Username);

            if (checkExists != null)
            {
                return BadRequest("User already exists");
            }

            string hashedPassword = _encoder.Encode(user.Password);

            var newUser = new User {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Username = user.Username,
                Password = hashedPassword
            };

            _db.User.Add(newUser);
            _db.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}