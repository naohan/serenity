using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.MoodMetrics.Commands;
using serenity.Application.Features.MoodMetrics.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MoodMetricsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoodMetricsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MoodMetricDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var moodMetrics = await _mediator.Send(new GetAllMoodMetricsQuery(), cancellationToken);
            return Ok(moodMetrics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las métricas de ánimo", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MoodMetricDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var moodMetric = await _mediator.Send(new GetMoodMetricByIdQuery(id), cancellationToken);
            if (moodMetric is null)
            {
                return NotFound();
            }

            return Ok(moodMetric);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la métrica de ánimo", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<MoodMetricDto>> Create([FromBody] CreateMoodMetricRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var moodMetric = await _mediator.Send(new CreateMoodMetricCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = moodMetric.Id }, moodMetric);
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
            return StatusCode(500, new { message = "Error al crear la métrica de ánimo", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MoodMetricDto>> Update(int id, [FromBody] UpdateMoodMetricRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var moodMetric = await _mediator.Send(new UpdateMoodMetricCommand(id, request), cancellationToken);
            return Ok(moodMetric);
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
            return StatusCode(500, new { message = "Error al actualizar la métrica de ánimo", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteMoodMetricCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la métrica de ánimo", error = ex.Message });
        }
    }
}




