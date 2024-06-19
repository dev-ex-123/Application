using Microsoft.AspNetCore.Mvc;
using CandidateApplication.Contract;
using CandidateApplication.Services;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost("CreateQuestion")]
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionDTO questionDto)
    {
        var question = await _questionService.CreatequestionAsync(questionDto);
        return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
    }

    [HttpGet("GetQuestionById/{id}")]
    public async Task<IActionResult> GetQuestionById(string id)
    {
        var question = await _questionService.GetQuestionByIdAsync(id);
        if (question == null) return NotFound();
        return Ok(question);
    }

    [HttpPut("UpdateQuestion/{id}")]
    public async Task<IActionResult> UpdateQuestion(string id, [FromBody] QuestionDTO questionDto)
    {
        var updatedQuestion = await _questionService.UpdatequestionAsync(id, questionDto);
        if (updatedQuestion == null) return NotFound();
        return Ok(updatedQuestion);
    }

    [HttpDelete("DeleteQuestion/{id}")]
    public async Task<IActionResult> DeleteQuestion(string id)
    {
        var result = await _questionService.DeleteQuestionAsync(id);
        if (!result) 
            return NotFound();
        return NoContent();
    }
}
