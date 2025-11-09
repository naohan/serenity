using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Trainers.Commands;
using serenity.Application.Features.Trainers.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TrainersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerDto>>> GetAll(CancellationToken cancellationToken)
    {
        var trainers = await _mediator.Send(new GetAllTrainersQuery(), cancellationToken);
        return Ok(trainers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrainerDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var trainer = await _mediator.Send(new GetTrainerByIdQuery(id), cancellationToken);
        if (trainer is null)
        {
            return NotFound();
        }

        return Ok(trainer);
    }

    [HttpPost]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] CreateTrainerRequest request, CancellationToken cancellationToken)
    {
        var trainer = await _mediator.Send(new CreateTrainerCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = trainer.Id }, trainer);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TrainerDto>> Update(int id, [FromBody] UpdateTrainerRequest request, CancellationToken cancellationToken)
    {
        var trainer = await _mediator.Send(new UpdateTrainerCommand(id, request), cancellationToken);
        return Ok(trainer);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTrainerCommand(id), cancellationToken);
        return NoContent();
    }
}

