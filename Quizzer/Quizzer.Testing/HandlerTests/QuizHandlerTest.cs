using Moq;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;
using Xunit;

namespace Quizzer.Testing.HandlerTests;

public class QuizHandlerTest
{
    private readonly Mock<IQuizRepository> _quizRepositoryMock;
    private readonly QuizHandler _quizHandler;
    private readonly List<Quiz> _quizzes;
    
    public QuizHandlerTest()
    {
        _quizRepositoryMock = new Mock<IQuizRepository>();
        _quizHandler = new QuizHandler(_quizRepositoryMock.Object);
        _quizzes = new List<Quiz>
        {
            new Quiz
            {
                Title = "Quiz",
                Description = "Quiz Description"
            },
        };
    }
    
    [Fact]
    public void List_QuizzesExist_ReturnsMappedQuiz()
    {
        // Arrange
        _quizRepositoryMock.Setup(repo => repo.List()).Returns(_quizzes);
        var role = Role.Restricted;

        // Act
        var result = _quizHandler.List(role);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsAssignableFrom<IEnumerable<QuizInfo>>(result);
    }
    
    [Fact]
    public void List_QuizzesExist_RestrictedRole_DoesNotCallIncludeMethod()
    {
        // Arrange
        _quizRepositoryMock.Setup(repo => repo.List()).Returns(_quizzes);
        var role = Role.Restricted;

        // Act
        _quizHandler.List(role);

        // Assert
        _quizRepositoryMock.Verify(repo => repo.List(), Times.Once);
        _quizRepositoryMock.Verify(repo => repo.ListIncludeAnswers(), Times.Never);
    }
    
    [Theory]
    [InlineData(Role.Edit)]
    [InlineData(Role.View)]
    public void List_QuizzesExist_NonRestrictedRole_CallsIncludeMethod(Role role)
    {
        // Arrange
        _quizRepositoryMock.Setup(repo => repo.ListIncludeAnswers()).Returns(_quizzes);

        // Act
        _quizHandler.List(role);

        // Assert
        _quizRepositoryMock.Verify(repo => repo.List(), Times.Never);
        _quizRepositoryMock.Verify(repo => repo.ListIncludeAnswers(), Times.Once);
    }

    [Fact]
    public void List_NoQuizzesExist_ThrowsInvalidOperationException()
    {
        // Arrange
        _quizRepositoryMock.Setup(repo => repo.List()).Returns(new List<Quiz>());
        var role = Role.Restricted;

        // Act Assert
        Assert.Throws<InvalidOperationException>(() => _quizHandler.List(role));
    }
}