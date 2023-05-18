using Quizzer.Interfaces;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Handlers;

public class QuestionHandler : IQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IQuizRepository _quizRepository;

    public QuestionHandler(IQuestionRepository questionRepository, IQuizRepository quizRepository)
    {
        _questionRepository = questionRepository;
        _quizRepository = quizRepository;
    }

    public void Add(QuestionInfo question, Guid quizId)
    {
        var quiz = _quizRepository.GetById(quizId);

        if (quiz == null)
            throw new ArgumentException("Quiz does not exist");

        //_questionRepository.Add(question);
        
        if (quiz.Questions.Any(q => q.QuestionIndex == question.QuestionIndex))
        {
            
        }
    }

    public void Delete(Guid questionId)
    {
        _questionRepository.Delete(questionId);
        //_questionRepository.ReindexQuestionNumbers(existingQuestion.QuizId);
    }

    public void Update(QuestionInfo questionInfo)
    {
        var question = _questionRepository.GetById(questionInfo.Id);
        
        // Check if the question number has changed
        var existingQuestion = _questionRepository.GetById(question.Id);
        if (existingQuestion != null && existingQuestion.QuestionIndex != question.QuestionIndex)
        {
            _questionRepository.Upsert(question);
            _questionRepository.ReindexQuestionNumbers(existingQuestion.QuizId);
        }
        else
        {
            _questionRepository.Upsert(question);
        }
        
        _questionRepository.Upsert(question);
    }
    
    
}
