using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Prescriptions.Commands;
using serenity.Application.Features.Prescriptions.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PrescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var prescriptions = await _mediator.Send(new GetAllPrescriptionsQuery(), cancellationToken);
            return Ok(prescriptions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las prescripciones", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PrescriptionDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var prescription = await _mediator.Send(new GetPrescriptionByIdQuery(id), cancellationToken);
            if (prescription is null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la prescripción", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<PrescriptionDto>> Create([FromBody] CreatePrescriptionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var prescription = await _mediator.Send(new CreatePrescriptionCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = prescription.Id }, prescription);
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
            return StatusCode(500, new { message = "Error al crear la prescripción", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PrescriptionDto>> Update(int id, [FromBody] UpdatePrescriptionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var prescription = await _mediator.Send(new UpdatePrescriptionCommand(id, request), cancellationToken);
            return Ok(prescription);
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
            return StatusCode(500, new { message = "Error al actualizar la prescripción", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeletePrescriptionCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la prescripción", error = ex.Message });
        }
    }
}




