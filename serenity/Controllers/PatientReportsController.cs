using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.PatientReports.Commands;
using serenity.Application.Features.PatientReports.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientReportDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var reports = await _mediator.Send(new GetAllPatientReportsQuery(), cancellationToken);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los reportes", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientReportDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var report = await _mediator.Send(new GetPatientReportByIdQuery(id), cancellationToken);
            if (report is null)
            {
                return NotFound();
            }

            return Ok(report);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el reporte", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<PatientReportDto>> Create([FromBody] CreatePatientReportRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var report = await _mediator.Send(new CreatePatientReportCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
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
            return StatusCode(500, new { message = "Error al crear el reporte", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientReportDto>> Update(int id, [FromBody] UpdatePatientReportRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var report = await _mediator.Send(new UpdatePatientReportCommand(id, request), cancellationToken);
            return Ok(report);
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
            return StatusCode(500, new { message = "Error al actualizar el reporte", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeletePatientReportCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el reporte", error = ex.Message });
        }
    }
}




