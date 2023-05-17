using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Moq;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Models.Bases;
using Quizzer.Models.Entities;
using Quizzer.Models.Enums;
using Quizzer.Testing.Helpers;
using Xunit;

namespace Quizzer.Testing.HandlerTests
{
    public class LoginHandlerUnitTest
    {
        private readonly IJwtTokenTestHelper _tokenHelper;
        private readonly LoginHandler _loginHandler;
        private readonly User _user;
        private readonly Mock<IScryptEncoder> _scryptEncoderMock;

        private readonly string _hashedPassword;

        public LoginHandlerUnitTest()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            _scryptEncoderMock = new Mock<IScryptEncoder>();
            _tokenHelper = new JwtTokenTestHelper();
            _hashedPassword = "$s2$16384$8$1$CWEwaFyddOON2eXheI7bE9m3us91F6zLRUwgIYTFvUM=$+oVlagLlAgr3Cn56DAEMrkgsfA+yfDNB316/wbuwrMk=";

            _user = new User {
                Id = Guid.Empty, 
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com", 
                Role = Role.Restricted, 
                Username = "testuser", 
                Password = "password"
            };
            
            var userHashed = new User {
                Id = Guid.Empty, 
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com", 
                Role = Role.Restricted, 
                Username = "testuser", 
                Password = _hashedPassword
            };

            userRepositoryMock.Setup(us => us.GetByUsername(It.Is<string>(s => s == _user.Username))).Returns(userHashed);

            _loginHandler = new LoginHandler(userRepositoryMock.Object, config, _scryptEncoderMock.Object);
        }

        [Fact]
        public void GeneratesValidJwtToken_VerifiesClaims()
        {
            // Arrange
            var jwtHandler = new JwtSecurityTokenHandler();

            // Act
            var token = _loginHandler.Generate(_user);

            // Assert
            Assert.NotNull(token);
            Assert.Matches(_tokenHelper.EncodingRegex, token);
            Assert.Equal(_tokenHelper.DotRegex.Matches(token).Count, _tokenHelper.ExpectedDotCount);
            Assert.Equal(token.Split('.')[0], _tokenHelper.Hs256JwtHeader);

            var claims = (jwtHandler.ReadToken(token) as JwtSecurityToken)?.Claims;

            var enumerable = claims as Claim[] ?? claims?.ToArray();
            Assert.Equal(_user.Username, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "nameidentifier").Value);
            Assert.Equal(_user.Email, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "emailaddress").Value);
            Assert.Equal(_user.FirstName, enumerable?.First(claim => claim.Type == _tokenHelper.XmlSoapClaimPrefix + "givenname").Value);
            Assert.Equal(_user.Role.ToString(), enumerable?.First(claim => claim.Type == _tokenHelper.MicrosoftClaimPrefix + "role").Value);

        }

        [Fact]
        public void Authenticates_MatchingUserLogin_ReturnsUser()
        {
            // Arrange
            var userLogin = new UserLogin {
                Id = Guid.Empty, 
                Username = _user.Username, 
                Password = _user.Password
            };

            _scryptEncoderMock.Setup(x => x.Compare(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            // Act
            var user = _loginHandler.Authenticate(userLogin);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(_user.Id, user?.Id);
            Assert.Equal(_user.FirstName, user?.FirstName);
            Assert.Equal(_user.LastName, user?.LastName);
            Assert.Equal(_user.Email, user?.Email);
            Assert.Equal(_user.Role, user?.Role);
            Assert.Equal(_user.Username, user?.Username);
            Assert.Equal(_hashedPassword, user?.Password);
        }

        [Fact]
        public void DoesNotAuthenticate_NonMatchingUserLogin_ReturnsNull()
        {
            // Arrange
            var userLogin = new UserLogin {
                Id = Guid.NewGuid(),
                Username = _user.Username,
                Password = "incorrect.password"
            };

            // Act
            var user = _loginHandler.Authenticate(userLogin);

            // Assert
            Assert.Null(user);
        }
    }
}