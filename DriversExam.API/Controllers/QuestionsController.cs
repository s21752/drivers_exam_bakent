using DriversExam.Core.DTO;
using DriversExam.Core.Services;
using DriversExam.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Cors;
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
    public async Task<IActionResult> CreateNewQuestion([FromForm] AddNewQuestionRequestDto dto)
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

    [HttpGet("GetCount")]
    public async Task<IActionResult> GetQuestionCount()
    {
        return Ok(await _questionsService.CountQuestionsAsync());
    }
    
    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int questionId)
    {
        try
        {
            return Ok(await _questionsService.GetByIdAsync(questionId));
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetSomeRandom")]
    public async Task<IActionResult> GetQuantityOfRandomQuestions(int quantity)
    {
        var questions = await _questionsService.GetManyRandomQuestionsAsync(quantity);
        return Ok(questions);
    }

    [HttpGet("GetPaginated")]
    public async Task<IActionResult> GetPaginatedQuestions(int pageSize, int pageIndex)
    {
        var questions = await _questionsService.GetPaginatedQuestionsAsync(pageSize, pageIndex);
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
    [EnableCors]
    [HttpPatch("Update")]
    public async Task<IActionResult> UpdateQuestion([FromForm] UpdateQuestionRequestDto dto)
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