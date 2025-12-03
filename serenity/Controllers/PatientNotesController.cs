using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.PatientNotes.Commands;
using serenity.Application.Features.PatientNotes.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientNotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientNotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientNoteDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var notes = await _mediator.Send(new GetAllPatientNotesQuery(), cancellationToken);
            return Ok(notes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las notas", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientNoteDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _mediator.Send(new GetPatientNoteByIdQuery(id), cancellationToken);
            if (note is null)
            {
                return NotFound();
            }

            return Ok(note);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la nota", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<PatientNoteDto>> Create([FromBody] CreatePatientNoteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _mediator.Send(new CreatePatientNoteCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
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
            return StatusCode(500, new { message = "Error al crear la nota", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientNoteDto>> Update(int id, [FromBody] UpdatePatientNoteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var note = await _mediator.Send(new UpdatePatientNoteCommand(id, request), cancellationToken);
            return Ok(note);
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
            return StatusCode(500, new { message = "Error al actualizar la nota", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeletePatientNoteCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la nota", error = ex.Message });
        }
    }
}




