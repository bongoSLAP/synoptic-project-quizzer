using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Handlers;

public class QuestionHandler : IQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IAnswerRepository _answerRepository;

    public QuestionHandler(IQuestionRepository questionRepository, IQuizRepository quizRepository, IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _quizRepository = quizRepository;
        _answerRepository = answerRepository;
    }

    public void Add(QuestionInfo questionInfo, Guid quizId)
    {
        if (questionInfo.Answers.Count < 2)
            throw new InvalidOperationException("A question requires 2 or more answers.");

        var quiz = _quizRepository.GetById(quizId);

        var question = new Question()
        {
            Text = questionInfo.Text,
            QuestionIndex = questionInfo.QuestionIndex,
            QuizId = quizId,
            Quiz = quiz
        };

        _questionRepository.Add(question);

        foreach (var answerInfo in questionInfo.Answers)
        {
            var answer = new Answer()
            {
                Text = answerInfo.Text,
                AnswerIndex = answerInfo.AnswerIndex,
                QuestionId = question.Id,
                Question = question
            };

            _answerRepository.Add(answer);
        }

        if (questionInfo.Answers.GroupBy(ai => ai.AnswerIndex).Any(g => g.Count() > 1))
            _answerRepository.ReindexAnswerNumbers(question.Id);

        if (quiz.Questions.Any(q => q.QuestionIndex == question.QuestionIndex))
            _questionRepository.ReindexQuestionNumbers(quizId);
    }
    
    public void Delete(Guid questionId)
    {
        var question = _questionRepository.GetById(questionId);

        var quizId = question.QuizId;
        var quiz = _quizRepository.GetById(quizId);

        if (quiz.Questions.Count <= 3)
            throw new InvalidOperationException("This quiz will not have enough questions if this quiz is deleted.");
        
        _questionRepository.Delete(questionId);
        _questionRepository.ReindexQuestionNumbers(quizId);
    }

    public void Edit(QuestionInfo questionInfo)
    {
        var existingQuestion = _questionRepository.GetById(questionInfo.Id);
        existingQuestion.Text = questionInfo.Text;
        existingQuestion.QuestionIndex = questionInfo.QuestionIndex;
        
        _questionRepository.Upsert(existingQuestion);
        
        if (questionInfo.QuestionIndex != existingQuestion.QuestionIndex)
            _questionRepository.ReindexQuestionNumbers(existingQuestion.Id);
    }
}
