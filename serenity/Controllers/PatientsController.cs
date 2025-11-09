using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.Patients.Commands;
using serenity.Application.Features.Patients.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientDto>>> GetAll(CancellationToken cancellationToken)
    {
        var patients = await _mediator.Send(new GetAllPatientsQuery(), cancellationToken);
        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PatientDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var patient = await _mediator.Send(new GetPatientByIdQuery(id), cancellationToken);
        if (patient is null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult<PatientDto>> Create([FromBody] CreatePatientRequest request, CancellationToken cancellationToken)
    {
        var patient = await _mediator.Send(new CreatePatientCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PatientDto>> Update(int id, [FromBody] UpdatePatientRequest request, CancellationToken cancellationToken)
    {
        var patient = await _mediator.Send(new UpdatePatientCommand(id, request), cancellationToken);
        return Ok(patient);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePatientCommand(id), cancellationToken);
        return NoContent();
    }
}

