using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.InsightsAndRecommendations.Commands;
using serenity.Application.Features.InsightsAndRecommendations.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InsightsAndRecommendationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InsightsAndRecommendationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsightsAndRecommendationDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var insights = await _mediator.Send(new GetAllInsightsAndRecommendationsQuery(), cancellationToken);
            return Ok(insights);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los insights", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InsightsAndRecommendationDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var insight = await _mediator.Send(new GetInsightsAndRecommendationByIdQuery(id), cancellationToken);
            if (insight is null)
            {
                return NotFound();
            }

            return Ok(insight);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el insight", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<InsightsAndRecommendationDto>> Create([FromBody] CreateInsightsAndRecommendationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var insight = await _mediator.Send(new CreateInsightsAndRecommendationCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = insight.Id }, insight);
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
            return StatusCode(500, new { message = "Error al crear el insight", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<InsightsAndRecommendationDto>> Update(int id, [FromBody] UpdateInsightsAndRecommendationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var insight = await _mediator.Send(new UpdateInsightsAndRecommendationCommand(id, request), cancellationToken);
            return Ok(insight);
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
            return StatusCode(500, new { message = "Error al actualizar el insight", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteInsightsAndRecommendationCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el insight", error = ex.Message });
        }
    }
}



