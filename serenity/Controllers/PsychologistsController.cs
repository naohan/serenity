using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Psychologists.Commands;
using serenity.Application.Features.Psychologists.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PsychologistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PsychologistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PsychologistDto>>> GetAll(CancellationToken cancellationToken)
    {
        var psychologists = await _mediator.Send(new GetAllPsychologistsQuery(), cancellationToken);
        return Ok(psychologists);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PsychologistDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var psychologist = await _mediator.Send(new GetPsychologistByIdQuery(id), cancellationToken);
        if (psychologist is null)
        {
            return NotFound();
        }

        return Ok(psychologist);
    }

    [HttpPost]
    public async Task<ActionResult<PsychologistDto>> Create([FromBody] CreatePsychologistRequest request, CancellationToken cancellationToken)
    {
        var psychologist = await _mediator.Send(new CreatePsychologistCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = psychologist.Id }, psychologist);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PsychologistDto>> Update(int id, [FromBody] UpdatePsychologistRequest request, CancellationToken cancellationToken)
    {
        var psychologist = await _mediator.Send(new UpdatePsychologistCommand(id, request), cancellationToken);
        return Ok(psychologist);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePsychologistCommand(id), cancellationToken);
        return NoContent();
    }
}

