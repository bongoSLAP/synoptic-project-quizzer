using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;
using Quizzer.Validators;

namespace Quizzer.Controllers;

[Route("Answer")]
public class AnswerController : Controller
{
    private readonly IAnswerHandler _answerHandler;
    private readonly IValidator<AnswerInfo> _validator;
    
    public AnswerController(IAnswerHandler answerHandler, IValidator<AnswerInfo> validator)
    {
        _answerHandler = answerHandler;
        _validator = validator;
    }

    [Authorize(Roles = "Edit")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] AnswerAddInfo info)
    {
        var validationResult = _validator.Validate(info.AnswerInfo);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        try
        {
            _answerHandler.Add(info.AnswerInfo, info.QuestionId);
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
            _answerHandler.Delete(data.Id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = "Edit")]
    [HttpPost("Edit")]
    public IActionResult Edit([FromBody] AnswerInfo info)
    {
        var validationResult = _validator.Validate(info);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        try
        {
            _answerHandler.Edit(info);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

