using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.MentalWellbeingMetrics.Commands;
using serenity.Application.Features.MentalWellbeingMetrics.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MentalWellbeingMetricsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MentalWellbeingMetricsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MentalWellbeingMetricDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var metrics = await _mediator.Send(new GetAllMentalWellbeingMetricsQuery(), cancellationToken);
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las métricas de bienestar mental", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MentalWellbeingMetricDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var metric = await _mediator.Send(new GetMentalWellbeingMetricByIdQuery(id), cancellationToken);
            if (metric is null)
            {
                return NotFound();
            }

            return Ok(metric);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la métrica de bienestar mental", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<MentalWellbeingMetricDto>> Create([FromBody] CreateMentalWellbeingMetricRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var metric = await _mediator.Send(new CreateMentalWellbeingMetricCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = metric.Id }, metric);
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
            return StatusCode(500, new { message = "Error al crear la métrica de bienestar mental", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MentalWellbeingMetricDto>> Update(int id, [FromBody] UpdateMentalWellbeingMetricRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var metric = await _mediator.Send(new UpdateMentalWellbeingMetricCommand(id, request), cancellationToken);
            return Ok(metric);
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
            return StatusCode(500, new { message = "Error al actualizar la métrica de bienestar mental", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteMentalWellbeingMetricCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la métrica de bienestar mental", error = ex.Message });
        }
    }
}




