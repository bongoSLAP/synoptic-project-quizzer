using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Quizzer.Controllers;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Models.Bases;
using Quizzer.Models.Entities;
using Quizzer.Models.Enums;
using Quizzer.Testing.Helpers;
using Xunit;

namespace Quizzer.Testing.ControllerTests
{
    public class LoginControllerIntegrationTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IScryptEncoder> _mockScryptEncoder;
        private readonly UserLogin _userLogin;
        private readonly IJwtTokenTestHelper _tokenHelper;

        public LoginControllerIntegrationTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockScryptEncoder = new Mock<IScryptEncoder>();
            _tokenHelper = new JwtTokenTestHelper();

            _userLogin = new UserLogin {
                Id = Guid.NewGuid(), 
                Username = "testuser", 
                Password = "testpassword"
            };
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsValidToken()
        {
            // Arrange
            var user = new User {
                Id = Guid.NewGuid(), 
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com", 
                Role = Role.Student, 
                Username = "testuser", 
                Password = "hashedPassword"
            };
            
            _mockUserRepository.Setup(r => r.GetByUsername(_userLogin.Username)).Returns(user);
            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("vaOFjM01gSMVwKjrzfT8ofAypG9cQi77uVl161ow"); //figure out how to hide this when conn string etc is hidden
            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("https://localhost:7173/");
            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Audience")]).Returns("https://localhost:7173/");
            _mockScryptEncoder.Setup(encoder => encoder.Compare(_userLogin.Password, "hashedPassword")).Returns(true);
            
            var loginHandler = new LoginHandler(_mockUserRepository.Object, _mockConfiguration.Object, _mockScryptEncoder.Object);
            var loginController = new LoginController(
                loginHandler
            );
            
            var jwtHandler = new JwtSecurityTokenHandler();

            // Act
            var result = loginController.Login(_userLogin);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var resultValue = ((OkObjectResult)result).Value;
            
            Assert.NotNull(resultValue);
            var token = resultValue.ToString();
            
            Assert.Matches(_tokenHelper.EncodingRegex, token);
            Assert.NotNull(token);
            Assert.Equal(_tokenHelper.DotRegex.Matches(token).Count, _tokenHelper.ExpectedDotCount);
            Assert.Equal(token.Split('.')[0], _tokenHelper.Hs256JwtHeader);

            var claims = (jwtHandler.ReadToken(token) as JwtSecurityToken)?.Claims;

            var enumerable = claims as Claim[] ?? claims?.ToArray();
            Assert.Equal(user.Username, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "nameidentifier").Value);
            Assert.Equal(user.Email, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "emailaddress").Value);
            Assert.Equal(user.FirstName, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "givenname").Value);
            Assert.Equal(user.Role.ToString(), enumerable?.First(claim => claim.Type == _tokenHelper.MicrosoftClaimPrefix + "role").Value);
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByUsername(_userLogin.Username)).Returns((User?)null);
            
            var loginHandler = new LoginHandler(_mockUserRepository.Object, _mockConfiguration.Object, _mockScryptEncoder.Object);
            var loginController = new LoginController(
                loginHandler
            );

            // Act
            var result = loginController.Login(_userLogin);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}