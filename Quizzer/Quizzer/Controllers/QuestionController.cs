using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizzer.Interfaces;
using Quizzer.Models;
using Quizzer.Models.Entities.Info;

namespace Quizzer.Controllers;

[Route("Question")]
public class QuestionController : Controller
{
    private readonly IQuestionHandler _questionHandler;
    public QuestionController(IQuestionHandler questionHandler)
    {
        _questionHandler = questionHandler;
    }

    [Authorize(Roles = "Edit")]
    [HttpPost("Add")]
    public IActionResult Add([FromBody] QuestionAddInfo info)
    {
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