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

    public SignupController(IUserRepository userRepository, IScryptEncoder encoder)
    {
        _userRepository = userRepository;
        _encoder = encoder;
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

            _userRepository.Add(newUser);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}