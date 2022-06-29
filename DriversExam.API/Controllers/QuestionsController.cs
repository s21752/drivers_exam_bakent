using DriversExam.Core.DTO;
using DriversExam.Core.Services;
using DriversExam.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DriversExam.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionsService _questionsService;

    public QuestionsController(IQuestionsService questionsService)
    {
        _questionsService = questionsService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateNewQuestion([FromBody] AddNewQuestionRequestDto dto)
    {
        try
        {
            await _questionsService.AddNewQuestionAsync(dto);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
        catch (EntityAlreadyExistingException)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetSome")]
    public async Task<IActionResult> GetQuantityOfQuestions(int? quantity)
    {
        var questions = await _questionsService.GetNumberOfQuestionsAsync(quantity);
        return Ok(questions);
    }
    
    [HttpGet("GetRandom")]
    public async Task<IActionResult> GetRandomQuestion()
    {
        var randomQuestion = await _questionsService.GetRandomQuestionAsync();
        return Ok(randomQuestion);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteById(int id)
    {
        try
        {
            await _questionsService.DeleteQuestionAsync(id);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
        
    }
    
    // I am using Patch instead of Put, because I've implemented it in such a way that I can change single field if I want
    [HttpPatch("Update")]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequestDto dto)
    {
        try
        {
            await _questionsService.UpdateQuestionAsync(dto);
            return NoContent();
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
    }
}