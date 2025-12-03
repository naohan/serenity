using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.MeditationSessions.Commands;
using serenity.Application.Features.MeditationSessions.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeditationSessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeditationSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeditationSessionDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var sessions = await _mediator.Send(new GetAllMeditationSessionsQuery(), cancellationToken);
            return Ok(sessions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las sesiones de meditación", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MeditationSessionDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _mediator.Send(new GetMeditationSessionByIdQuery(id), cancellationToken);
            if (session is null)
            {
                return NotFound();
            }

            return Ok(session);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la sesión de meditación", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<MeditationSessionDto>> Create([FromBody] CreateMeditationSessionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _mediator.Send(new CreateMeditationSessionCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
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
            return StatusCode(500, new { message = "Error al crear la sesión de meditación", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MeditationSessionDto>> Update(int id, [FromBody] UpdateMeditationSessionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _mediator.Send(new UpdateMeditationSessionCommand(id, request), cancellationToken);
            return Ok(session);
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
            return StatusCode(500, new { message = "Error al actualizar la sesión de meditación", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteMeditationSessionCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la sesión de meditación", error = ex.Message });
        }
    }
}



