using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.SupportMessage;
using DentifyBackend.Odontology.Domain.Services.SupportMessage;
using DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;
using DentifyBackend.Odontology.Interfaces.REST.Transform.SupportMessage;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Controllers;

[ApiController]
[Route("api/support-message")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Support Message")]
public class SupportMessageController(
    ISupportMessageCommandService supportMessageCommandService,
    ISupportMessageQueryService supportMessageQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a support message",
        Description = "Create a support message with a given data",
        OperationId = "CreateSupportMessage")]
    [SwaggerResponse(201, "The support message was created", typeof(SupportMessageResource))]
    [SwaggerResponse(400, "The support message was not created")]
    public async Task<ActionResult> CreateSupportMessage([FromBody] CreateSupportMessageResource resource)
    {
        var createSupportMessageCommand =
            CreateSupportMessageCommandFromResourceAssembler.toCommandFromResource(resource);
        var result = await supportMessageCommandService.Handle(createSupportMessageCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetSupportMessageById), new { result.id },
            SupportMessageFromEntityAssembler.toResourceFromEntity(result));
    }


    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a support message by id",
        Description = "Get a support message for a given identifier",
        OperationId = "GetSupportMessageById")]
    [SwaggerResponse(200, "The support message was found", typeof(SupportMessageResource))]
    [SwaggerResponse(404, "The support message was not found")]
    public async Task<ActionResult> GetSupportMessageById(int id)
    {
        var getSupportMessageByIdQuery = new GetSupportMessageByIdQuery(id);
        var result = await supportMessageQueryService.Handle(getSupportMessageByIdQuery);
        if (result is null) return NotFound();
        var resource = SupportMessageFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
}