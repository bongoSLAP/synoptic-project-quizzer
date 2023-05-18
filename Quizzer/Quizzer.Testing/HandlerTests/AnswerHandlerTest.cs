using Moq;
using Quizzer.Handlers;
using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;
using Xunit;

namespace Quizzer.Testing.HandlerTests;

public class AnswerHandlerTests
{
    private readonly Mock<IAnswerRepository> _answerRepositoryMock;
    private readonly Mock<IQuestionRepository> _questionRepositoryMock;
    private readonly AnswerHandler _answerHandler;

    public AnswerHandlerTests()
    {
        _answerRepositoryMock = new Mock<IAnswerRepository>();
        _questionRepositoryMock = new Mock<IQuestionRepository>();
        _answerHandler = new AnswerHandler(_answerRepositoryMock.Object, _questionRepositoryMock.Object);
    }

    [Fact]
    public void Add_AddIsCalled()
    {
        // Arrange
        var question = new Question { Id = Guid.NewGuid(), Answers = new List<Answer>() };
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };

        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act
        _answerHandler.Add(answerInfo, question.Id);

        // Assert
        _answerRepositoryMock.Verify(x => x.Add(It.IsAny<Answer>()), Times.Once);
    }

    [Fact]
    public void Add_DuplicateIndexes_ReIndexesAnswerIndexes()
    {
        // Arrange
        var question = new Question
            { Id = Guid.NewGuid(), Answers = new List<Answer>() { new Answer { AnswerIndex = 1 } } };
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };

        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act
        _answerHandler.Add(answerInfo, question.Id);

        // Assert
        _answerRepositoryMock.Verify(x => x.ReindexAnswerNumbers(question.Id), Times.Once);
    }

    [Fact]
    public void Delete_DeleteIsCalled()
    {
        // Arrange
        var question = new Question
            { Id = Guid.NewGuid(), Answers = new List<Answer>() { new Answer(), new Answer(), new Answer() } };
        var answer = new Answer { Id = Guid.NewGuid(), QuestionId = question.Id };

        _answerRepositoryMock.Setup(x => x.GetById(answer.Id)).Returns(answer);
        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act
        _answerHandler.Delete(answer.Id);

        // Assert
        _answerRepositoryMock.Verify(x => x.Delete(answer.Id), Times.Once);
    }

    [Fact]
    public void Delete_AnswerBelongsToQuestionWithTwoAnswers_ThrowsInvalidOperationException()
    {
        // Arrange
        var question = new Question
            { Id = Guid.NewGuid(), Answers = new List<Answer>() { new Answer(), new Answer() } };
        var answer = new Answer { Id = Guid.NewGuid(), QuestionId = question.Id };

        _answerRepositoryMock.Setup(x => x.GetById(answer.Id)).Returns(answer);
        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _answerHandler.Delete(answer.Id));
    }

    [Fact]
    public void Edit_EditIsCalled()
    {
        // Arrange
        var question = new Question { Id = Guid.NewGuid(), Answers = new List<Answer>() };
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };
        var existingAnswer = new Answer { Id = answerInfo.Id, QuestionId = question.Id };

        _answerRepositoryMock.Setup(x => x.GetById(answerInfo.Id)).Returns(existingAnswer);
        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act
        _answerHandler.Edit(answerInfo);

        // Assert
        _answerRepositoryMock.Verify(x => x.Upsert(It.IsAny<Answer>()), Times.Once);
    }

    [Fact]
    public void Edit_AnswerExistsWithSameIndex_ReIndexesAnswerIndexes()
    {
        // Arrange
        var question = new Question
            { Id = Guid.NewGuid(), Answers = new List<Answer>() { new Answer { AnswerIndex = 1 } } };
        var answerInfo = new AnswerInfo { Id = Guid.NewGuid(), Text = "Answer", AnswerIndex = 1 };
        var existingAnswer = new Answer { Id = answerInfo.Id, QuestionId = question.Id };

        _answerRepositoryMock.Setup(x => x.GetById(answerInfo.Id)).Returns(existingAnswer);
        _questionRepositoryMock.Setup(x => x.GetById(question.Id)).Returns(question);

        // Act
        _answerHandler.Edit(answerInfo);

        // Assert
        _answerRepositoryMock.Verify(x => x.ReindexAnswerNumbers(question.Id), Times.Once);
    }
}

