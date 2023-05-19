using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;
using Quizzer.Validators;

namespace Quizzer.Controllers;

[Route("Question")]
public class QuestionController : Controller
{
    private readonly IQuestionHandler _questionHandler;
    private readonly IValidator<QuestionInfo> _validator;
    public QuestionController(IQuestionHandler questionHandler, IValidator<QuestionInfo> validator)
    {
        _questionHandler = questionHandler;
        _validator = validator;
    }

    [Authorize(Roles = "Edit")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] QuestionAddInfo info)
    {
        var validationResult = _validator.Validate(info.QuestionInfo);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            _questionHandler.Add(info.QuestionInfo, info.QuizId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = "Edit")]
    [HttpPost("Delete")]
    public IActionResult Delete([FromBody] GuidRequest data)
    {
        try
        {
            _questionHandler.Delete(data.Id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = "Edit")]
    [HttpPost("Edit")]
    public IActionResult Edit([FromBody] QuestionInfo info)
    {
        var validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        try
        {
            _questionHandler.Edit(info);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}