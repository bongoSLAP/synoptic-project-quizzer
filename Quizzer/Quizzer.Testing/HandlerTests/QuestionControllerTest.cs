using FluentValidation;
using FluentValidation.Results;
using Moq;
using Quizzer.Controllers;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;
using Xunit;

namespace Quizzer.Testing.HandlerTests;

public class QuestionControllerTest
{
    private readonly Mock<IQuestionHandler> _questionHandlerMock;
    private readonly Mock<IValidator<QuestionInfo>> _validatorMock;
    private readonly QuestionController _questionController;

    public QuestionControllerTest()
    {
        _questionHandlerMock = new Mock<IQuestionHandler>();
        _validatorMock = new Mock<IValidator<QuestionInfo>>();
        _questionController = new QuestionController(_questionHandlerMock.Object, _validatorMock.Object);
    }

    [Fact]
    public void Add_CallsAddOnQuestionHandler()
    {
        // Arrange
        var questionInfo = new QuestionInfo { Id = Guid.NewGuid(), Text = "Question", QuestionIndex = 1 };
        var info = new QuestionAddInfo { QuestionInfo = questionInfo, QuizId = Guid.NewGuid() };
        _validatorMock.Setup(x => x.Validate(questionInfo)).Returns(new ValidationResult());

        // Act
        _questionController.Add(info);

        // Assert
        _questionHandlerMock.Verify(x => x.Add(questionInfo, info.QuizId), Times.Once);
    }

    [Fact]
    public void Delete_CallsDeleteOnQuestionHandler()
    {
        // Arrange
        var guidRequest = new GuidRequest { Id = Guid.NewGuid() };

        // Act
        _questionController.Delete(guidRequest);

        // Assert
        _questionHandlerMock.Verify(x => x.Delete(guidRequest.Id), Times.Once);
    }

    [Fact]
    public void Edit_CallsEditOnQuestionHandler()
    {
        // Arrange
        var questionInfo = new QuestionInfo { Id = Guid.NewGuid(), Text = "Question", QuestionIndex = 1 };
        _validatorMock.Setup(x => x.Validate(questionInfo)).Returns(new ValidationResult());

        // Act
        _questionController.Edit(questionInfo);

        // Assert
        _questionHandlerMock.Verify(x => x.Edit(questionInfo), Times.Once);
    }
}