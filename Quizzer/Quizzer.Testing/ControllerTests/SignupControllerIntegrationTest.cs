using Microsoft.AspNetCore.Mvc;
using Moq;
using Quizzer.Controllers;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Enums;
using Xunit;

namespace Quizzer.Testing.ControllerTests
{
    public class SignupControllerIntegrationTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IScryptEncoder> _mockScryptEncoder;
        private readonly User _user;

        public SignupControllerIntegrationTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockScryptEncoder = new Mock<IScryptEncoder>();

            _user = new User {
                Id = Guid.NewGuid(), 
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com", 
                Role = Role.Student, 
                Username = "testuser", 
                Password = "hashedPassword"
            };
        }

        [Fact]
        public void Create_ValidUser_ReturnsOkResult()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByUsername(_user.Username)).Returns((User?)null);
            _mockScryptEncoder.Setup(encoder => encoder.Encode(_user.Password)).Returns("hashedPassword");

            var signupController = new SignupController(
                _mockUserRepository.Object,
                _mockScryptEncoder.Object
            );

            // Act
            var result = signupController.Create(_user) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            _mockUserRepository.Verify(r => r.Add(It.Is<User>(u => 
                u.FirstName == _user.FirstName &&
                u.LastName == _user.LastName && 
                u.Email == _user.Email &&
                u.Role == _user.Role &&
                u.Username == _user.Username &&
                u.Password == "hashedPassword"
            )), Times.Once);
        }

        [Fact]
        public void Create_NullUser_ReturnsBadRequest()
        {
            // Arrange
            var signupController = new SignupController(
                _mockUserRepository.Object,
                _mockScryptEncoder.Object
            );

            // Act
            var result = signupController.Create(null) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User is null", result.Value);
        }

        [Fact]
        public void Create_ExistingUsername_ReturnsBadRequest()
        {
            // Arrange
            _mockUserRepository.Setup(r => r.GetByUsername(_user.Username)).Returns(_user);

            var signupController = new SignupController(
                _mockUserRepository.Object,
                _mockScryptEncoder.Object
            );

            // Act
            var result = signupController.Create(_user) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User already exists", result.Value);

            _mockUserRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Never);
        }
    }
}