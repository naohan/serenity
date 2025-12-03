using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serenity.Application.DTOs;
using serenity.Application.Features.ChatMessages.Commands;
using serenity.Application.Features.ChatMessages.Queries;

namespace serenity.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ChatMessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatMessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var messages = await _mediator.Send(new GetAllChatMessagesQuery(), cancellationToken);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener los mensajes", error = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ChatMessageDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var message = await _mediator.Send(new GetChatMessageByIdQuery(id), cancellationToken);
            if (message is null)
            {
                return NotFound();
            }

            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al obtener el mensaje", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<ChatMessageDto>> Create([FromBody] CreateChatMessageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var message = await _mediator.Send(new CreateChatMessageCommand(request), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
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
            return StatusCode(500, new { message = "Error al crear el mensaje", error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ChatMessageDto>> Update(int id, [FromBody] UpdateChatMessageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var message = await _mediator.Send(new UpdateChatMessageCommand(id, request), cancellationToken);
            return Ok(message);
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
            return StatusCode(500, new { message = "Error al actualizar el mensaje", error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteChatMessageCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error al eliminar el mensaje", error = ex.Message });
        }
    }
}



