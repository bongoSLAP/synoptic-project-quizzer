using Moq;
using Quizzer.Controllers;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;
using Xunit;

namespace Quizzer.Testing.ControllerTests;

public class AnswerControllerTest
{
    private readonly Mock<IAnswerHandler> _mockAnswerHandler;
    private readonly AnswerController _answerController;

    public AnswerControllerTest()
    {
        _mockAnswerHandler = new Mock<IAnswerHandler>();
        _answerController = new AnswerController(_mockAnswerHandler.Object);
    }

    [Fact]
    public void Add_WhenCalled_CallsAddOnAnswerHandler()
    {
        // Arrange
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };
        var info = new AnswerAddInfo { AnswerInfo = answerInfo, QuestionId = Guid.NewGuid() };

        // Act
        var result = _answerController.Add(info);

        // Assert
        _mockAnswerHandler.Verify(x => x.Add(answerInfo, info.QuestionId), Times.Once);
    }

    [Fact]
    public void Delete_CallsDeleteOnAnswerHandler()
    {
        // Arrange
        var id = Guid.NewGuid();
        var data = new GuidRequest { Id = id };

        // Act
        var result = _answerController.Delete(data);

        // Assert
        _mockAnswerHandler.Verify(x => x.Delete(id), Times.Once);
    }

    [Fact]
    public void Edit_CallsEditOnAnswerHandler()
    {
        // Arrange
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };

        // Act
        var result = _answerController.Edit(answerInfo);

        // Assert
        _mockAnswerHandler.Verify(x => x.Edit(answerInfo), Times.Once);
    }
}