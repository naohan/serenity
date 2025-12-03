using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Appointments.Commands;
using serenity.Application.Features.Appointments.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var appointments = await _mediator.Send(new GetAllAppointmentsQuery(), cancellationToken);
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener las citas", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppointmentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var appointment = await _mediator.Send(new GetAppointmentByIdQuery(id), cancellationToken);
            if (appointment is null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener la cita", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentDto>> Create([FromBody] CreateAppointmentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var appointment = await _mediator.Send(new CreateAppointmentCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
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
            return StatusCode(500, new { message = "Error al crear la cita", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AppointmentDto>> Update(int id, [FromBody] UpdateAppointmentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var appointment = await _mediator.Send(new UpdateAppointmentCommand(id, request), cancellationToken);
            return Ok(appointment);
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
            return StatusCode(500, new { message = "Error al actualizar la cita", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteAppointmentCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar la cita", error = ex.Message });
        }
    }
}



