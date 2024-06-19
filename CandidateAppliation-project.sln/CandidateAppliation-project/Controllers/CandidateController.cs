using Microsoft.AspNetCore.Mvc;
using CandidateApplication.Services;
using API.Contract;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private readonly ICandidateResponseService _candidateResponseService;
    private readonly IQuestionService _questionService;

    public CandidateController(ICandidateResponseService candidateResponseService, IQuestionService questionService)
    {
        _candidateResponseService = candidateResponseService;
        _questionService = questionService;
    }

    [HttpPost("Submit")]
    public async Task<IActionResult> SubmitResponse([FromBody] CandidateDTO candidateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await _candidateResponseService.SubmitdataAsync(candidateDto);
        return CreatedAtAction(nameof(GetResponseById), new { id = response.Id }, response);

    }
    [HttpGet("candidate/{applicationId}")]
    public async Task<IActionResult> GetApplicationForCandidate(string applicationId)
    {
        var application = await _questionService.GetQuestionsasync(applicationId);
        if (application == null)
        {
            return NotFound();
        }

        var candidateQuestions = application.Where(q => !q.IsInternal && !q.Hide).ToList();


        return Ok(candidateQuestions);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResponseById(string id)
    {
        var response = await _candidateResponseService.GetresponseByIdAsync(id);
        if (response == null) return NotFound();
        return Ok(response);
    }


}
