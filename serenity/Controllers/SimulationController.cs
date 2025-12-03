using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Simulation.Commands;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SimulationController : ControllerBase
{
    private readonly IMediator _mediator;

    public SimulationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Simulates stress levels throughout the day for a patient.
    /// </summary>
    [HttpPost("stress")]
    public async Task<ActionResult<SimulationResponseDto>> SimulateStress([FromBody] SimulateStressRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new SimulateStressCommand(request), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al simular niveles de estrés", error = ex.Message });
        }
    }

    /// <summary>
    /// Simulates wellbeing metrics for a patient.
    /// </summary>
    [HttpPost("wellbeing")]
    public async Task<ActionResult<SimulationResponseDto>> SimulateWellbeing([FromBody] SimulateWellbeingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new SimulateWellbeingCommand(request), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al simular métricas de bienestar", error = ex.Message });
        }
    }

    /// <summary>
    /// Simulates meditation sessions for a patient.
    /// </summary>
    [HttpPost("meditation")]
    public async Task<ActionResult<SimulationResponseDto>> SimulateMeditation([FromBody] SimulateMeditationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new SimulateMeditationCommand(request), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al simular sesiones de meditación", error = ex.Message });
        }
    }

    /// <summary>
    /// Simulates a full day of metrics for a patient (mood, emotions, wellbeing, meditation, stress).
    /// </summary>
    [HttpPost("full-day")]
    public async Task<ActionResult<SimulationResponseDto>> SimulateFullDay([FromBody] SimulateFullDayRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new SimulateFullDayCommand(request), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al simular día completo", error = ex.Message });
        }
    }
}

