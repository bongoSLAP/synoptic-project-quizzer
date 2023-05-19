using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;
using Quizzer.Models.Enums;

namespace Quizzer.Controllers;

[Route("Answer")]
public class AnswerController : Controller
{
    private readonly IAnswerHandler _answerHandler;
    public AnswerController(IAnswerHandler answerHandler)
    {
        _answerHandler = answerHandler;
    }

    [Authorize(Roles = "Edit")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] AnswerAddInfo info)
    {
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

