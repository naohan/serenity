using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.NoteSuggestions.Commands;
using serenity.Application.Features.NoteSuggestions.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NoteSuggestionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NoteSuggestionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteSuggestionDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var suggestions = await _mediator.Send(new GetAllNoteSuggestionsQuery(), cancellationToken);
            return Ok(suggestions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las sugerencias", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NoteSuggestionDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var suggestion = await _mediator.Send(new GetNoteSuggestionByIdQuery(id), cancellationToken);
            if (suggestion is null)
            {
                return NotFound();
            }

            return Ok(suggestion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la sugerencia", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<NoteSuggestionDto>> Create([FromBody] CreateNoteSuggestionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var suggestion = await _mediator.Send(new CreateNoteSuggestionCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = suggestion.Id }, suggestion);
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
            return StatusCode(500, new { message = "Error al crear la sugerencia", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<NoteSuggestionDto>> Update(int id, [FromBody] UpdateNoteSuggestionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var suggestion = await _mediator.Send(new UpdateNoteSuggestionCommand(id, request), cancellationToken);
            return Ok(suggestion);
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
            return StatusCode(500, new { message = "Error al actualizar la sugerencia", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteNoteSuggestionCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la sugerencia", error = ex.Message });
        }
    }
}




