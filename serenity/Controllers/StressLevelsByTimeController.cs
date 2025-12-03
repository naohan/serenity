using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.StressLevelsByTime.Commands;
using serenity.Application.Features.StressLevelsByTime.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StressLevelsByTimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public StressLevelsByTimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StressLevelsByTimeDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var stressLevels = await _mediator.Send(new GetAllStressLevelsByTimeQuery(), cancellationToken);
            return Ok(stressLevels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los niveles de estrés", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StressLevelsByTimeDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var stressLevel = await _mediator.Send(new GetStressLevelsByTimeByIdQuery(id), cancellationToken);
            if (stressLevel is null)
            {
                return NotFound();
            }

            return Ok(stressLevel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el nivel de estrés", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<StressLevelsByTimeDto>> Create([FromBody] CreateStressLevelsByTimeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var stressLevel = await _mediator.Send(new CreateStressLevelsByTimeCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = stressLevel.Id }, stressLevel);
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
            return StatusCode(500, new { message = "Error al crear el nivel de estrés", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<StressLevelsByTimeDto>> Update(int id, [FromBody] UpdateStressLevelsByTimeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var stressLevel = await _mediator.Send(new UpdateStressLevelsByTimeCommand(id, request), cancellationToken);
            return Ok(stressLevel);
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
            return StatusCode(500, new { message = "Error al actualizar el nivel de estrés", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteStressLevelsByTimeCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el nivel de estrés", error = ex.Message });
        }
    }
}




