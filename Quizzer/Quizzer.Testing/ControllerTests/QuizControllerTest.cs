using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Quizzer.Controllers;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;
using Xunit;

namespace Quizzer.Testing.ControllerTests;

public class QuizControllerTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IQuizHandler> _quizHandlerMock;
    private readonly Mock<HttpContext> _httpContextMock;
    private readonly QuizController _controller;
    private readonly User _user;

    public QuizControllerTest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _quizHandlerMock = new Mock<IQuizHandler>();
        _httpContextMock = new Mock<HttpContext>();
        _controller = new QuizController(_userRepositoryMock.Object, _quizHandlerMock.Object);
        _user = new User {
            Id = Guid.NewGuid(), 
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@example.com", 
            Role = Role.Restricted, 
            Username = "testuser", 
            Password = "hashedPassword"
        };
        
        _httpContextMock.SetupGet(context => context.User).Returns(new ClaimsPrincipal());
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContextMock.Object
        };

    }

    private IEnumerable<QuizInfo> GetDummyQuiz()
    {
        return new List<QuizInfo>
        {
            new QuizInfo { Title = "Quiz", Description = "Description" }
        };
    }
    
    [Fact]
    public void ListAll_AuthorizedUser_ReturnsOkResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByClaim(It.IsAny<ClaimsPrincipal>())).Returns(_user);
        _quizHandlerMock.Setup(handler => handler.List(_user.Role)).Returns(GetDummyQuiz());
        
        // Act
        var result = _controller.ListAll();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void ListAll_UnauthorizedUser_ReturnsUnauthorizedResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByClaim(It.IsAny<ClaimsPrincipal>())).Returns((User)null);

        // Act
        var result = _controller.ListAll();

        // Assert
        Assert.IsType<UnauthorizedObjectResult>(result);
    }

    [Fact]
    public void ListAll_NoQuizzesFound_ReturnsNoContentResult()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByClaim(It.IsAny<ClaimsPrincipal>())).Returns(_user);
        _quizHandlerMock.Setup(handler => handler.List(_user.Role)).Throws<InvalidOperationException>();

        // Act
        var result = _controller.ListAll();

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}