using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Quizzer.Interfaces;
using Quizzer.Models.Bases;
using Quizzer.Models.Entities;

namespace Quizzer.Handlers;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    private readonly IScryptEncoder _encoder;

    public LoginHandler(IUserRepository userRepository, IConfiguration config, IScryptEncoder encoder)
    {
        _userRepository = userRepository;
        _config = config;
        _encoder = encoder;
    }

    public string Generate(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(45),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User? Authenticate(UserLogin userLogin)
    {
        var currentUser = _userRepository.GetByUsername(userLogin.Username);

        if (currentUser == null) 
            return null;
        
        bool isPasswordMatch = _encoder.Compare(userLogin.Password, currentUser.Password);

        return isPasswordMatch ? currentUser : null;
    }
}
