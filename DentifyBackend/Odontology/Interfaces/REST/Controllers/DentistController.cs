using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.Dentist;
using DentifyBackend.Odontology.Domain.Services.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Dentist;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Controllers;

[ApiController]
[Route("api/dentists")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Dentists")]
public class DentistController(
    IDentistCommandService dentistCommandService,
    IDentistQueryService dentistQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a dentist",
        Description = "Create a dentist with a given data",
        OperationId = "CreateDentist")]
    [SwaggerResponse(201, "The dentist was created", typeof(DentistResource))]
    [SwaggerResponse(400, "The dentist was not created")]
    public async Task<ActionResult> CreateDentist([FromBody] CreateDentistResource resource)
    {
        var createDentistCommand = CreateDentistCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await dentistCommandService.Handle(createDentistCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetDentistById), new { result.id },
            DentistResourceFromEntityAssembler.ToResourceFromEntity(result));
    }


    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a dentist by id",
        Description = "Get a dentist for a given identifier",
        OperationId = "GetDentistById")]
    [SwaggerResponse(200, "The dentist was found", typeof(DentistResource))]
    [SwaggerResponse(404, "The dentist was not found")]
    public async Task<ActionResult> GetDentistById(int id)
    {
        var getDentistByIdQuery = new GetDentistByIdQuery(id);
        var result = await dentistQueryService.Handle(getDentistByIdQuery);
        if (result is null) return NotFound();
        var resource = DentistResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the dentists",
        Description = "Get all the dentists",
        OperationId = "GetAllDentists")]
    [SwaggerResponse(200, "Dentists were found", typeof(IEnumerable<DentistResource>))]
    [SwaggerResponse(204, "Dentists were not found")]
    public async Task<ActionResult> GetAllDentists()
    {
        var getAllDentistsQuery = new GetAllDentistsQuery();
        var result = await dentistQueryService.Handle(getAllDentistsQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(DentistResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }


    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a dentist",
        Description = "Update the personal information of an existing dentist",
        OperationId = "UpdateDentist")]
    [SwaggerResponse(200, "The dentist was updated successfully", typeof(DentistResource))]
    [SwaggerResponse(404, "The dentist was not found")]
    public async Task<ActionResult> UpdateDentist(int id, [FromBody] UpdateDentistResource resource)
    {
        var getDentistByIdQuery = new GetDentistByIdQuery(id);
        var existingDentist = await dentistQueryService.Handle(getDentistByIdQuery);
        if (existingDentist is null) return NotFound("The specified dentist could not be found.");

        var updateDentistCommand = UpdateDentistCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await dentistCommandService.Handle(updateDentistCommand, id);

        if (result is null)
            return BadRequest("The update command could not be processed, check the provided data.");

        var updatedDentistResource = DentistResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(updatedDentistResource);
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a dentist",
        Description = "Delete a dentist by its identifier",
        OperationId = "DeleteDentist")]
    [SwaggerResponse(204, "The dentist was deleted successfully")]
    [SwaggerResponse(404, "The dentist was not found")]
    public async Task<IActionResult> DeleteDentist(int id)
    {
        var deleteResource = new DeleteDentistResource(id);

        var deleteCommand = DeleteDentistCommandFromResourceAssembler.ToCommandFromResource(deleteResource);

        await dentistCommandService.Handle(deleteCommand);

        return NoContent();
    }
}