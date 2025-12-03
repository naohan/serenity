using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.EmotionalStates.Commands;
using serenity.Application.Features.EmotionalStates.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmotionalStatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmotionalStatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmotionalStateDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var emotionalStates = await _mediator.Send(new GetAllEmotionalStatesQuery(), cancellationToken);
            return Ok(emotionalStates);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los estados emocionales", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmotionalStateDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var emotionalState = await _mediator.Send(new GetEmotionalStateByIdQuery(id), cancellationToken);
            if (emotionalState is null)
            {
                return NotFound();
            }

            return Ok(emotionalState);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el estado emocional", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<EmotionalStateDto>> Create([FromBody] CreateEmotionalStateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var emotionalState = await _mediator.Send(new CreateEmotionalStateCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = emotionalState.Id }, emotionalState);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al crear el estado emocional", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmotionalStateDto>> Update(int id, [FromBody] UpdateEmotionalStateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var emotionalState = await _mediator.Send(new UpdateEmotionalStateCommand(id, request), cancellationToken);
            return Ok(emotionalState);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al actualizar el estado emocional", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteEmotionalStateCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el estado emocional", error = ex.Message });
        }
    }
}




