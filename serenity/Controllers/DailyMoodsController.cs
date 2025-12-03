using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.DailyMoods.Commands;
using serenity.Application.Features.DailyMoods.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DailyMoodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DailyMoodsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DailyMoodDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var dailyMoods = await _mediator.Send(new GetAllDailyMoodsQuery(), cancellationToken);
            return Ok(dailyMoods);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los estados de ánimo", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DailyMoodDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var dailyMood = await _mediator.Send(new GetDailyMoodByIdQuery(id), cancellationToken);
            if (dailyMood is null)
            {
                return NotFound();
            }

            return Ok(dailyMood);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el estado de ánimo", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<DailyMoodDto>> Create([FromBody] CreateDailyMoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var dailyMood = await _mediator.Send(new CreateDailyMoodCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = dailyMood.Id }, dailyMood);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al crear el estado de ánimo", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DailyMoodDto>> Update(int id, [FromBody] UpdateDailyMoodRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var dailyMood = await _mediator.Send(new UpdateDailyMoodCommand(id, request), cancellationToken);
            return Ok(dailyMood);
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
            return StatusCode(500, new { message = "Error al actualizar el estado de ánimo", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteDailyMoodCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el estado de ánimo", error = ex.Message });
        }
    }
}



