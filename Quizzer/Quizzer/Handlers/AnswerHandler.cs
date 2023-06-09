﻿using Quizzer.Interfaces;
using Quizzer.Models.Entities;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Handlers;

public class AnswerHandler : IAnswerHandler
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionRepository _questionRepository;
    
    public AnswerHandler(IAnswerRepository answerRepository, IQuestionRepository questionRepository)
    { 
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
    }
    
    public void Add(AnswerInfo answerInfo, Guid questionId)
    {
        var question = _questionRepository.GetById(questionId);

        var answer = new Answer()
        {
            Id = answerInfo.Id,
            Text = answerInfo.Text,
            Question = question,
            AnswerIndex = answerInfo.AnswerIndex,
            QuestionId = questionId
        };

        _answerRepository.Add(answer);

        if (question.Answers.Any(a => a.AnswerIndex == answerInfo.AnswerIndex))
            _answerRepository.ReindexAnswerNumbers(questionId);
    }

    public void Delete(Guid answerId)
    {
        var answer = _answerRepository.GetById(answerId);

        var questionId = answer.QuestionId;
        var question = _questionRepository.GetById(questionId);

        if (question.Answers.Count <= 2)
            throw new InvalidOperationException("This question will not have enough answers if this answer is deleted.");
        
        _answerRepository.Delete(answerId);
        _answerRepository.ReindexAnswerNumbers(questionId);
    }

    public void Edit(AnswerInfo answerInfo)
    {
        var existingAnswer = _answerRepository.GetById(answerInfo.Id);
        existingAnswer.Text = answerInfo.Text;
        existingAnswer.AnswerIndex = answerInfo.AnswerIndex;
        
        var question = _questionRepository.GetById(existingAnswer.QuestionId);
        
        if (question.Answers.Any(a => a.AnswerIndex == answerInfo.AnswerIndex))
        {
            _answerRepository.ReindexAnswerNumbers(question.Id);
            _answerRepository.Upsert(existingAnswer);
            return;
        }
        
        _answerRepository.Upsert(existingAnswer);

        

    }
}