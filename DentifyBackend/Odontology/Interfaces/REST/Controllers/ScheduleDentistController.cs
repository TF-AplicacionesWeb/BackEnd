using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.ScheduleDentist;
using DentifyBackend.Odontology.Domain.Services.ScheduleDentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;
using DentifyBackend.Odontology.Interfaces.REST.Transform.ScheduleDentist;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Controllers;

[ApiController]
[Route("api/schedule_dentists")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Schedules Dentists")]
public class ScheduleDentistController(
    IScheduleDentistCommandService scheduleDentistCommandService,
    IScheduleDentistQueryService scheduleDentistQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a schedule dentist",
        Description = "Create a schedule dentist with a given data",
        OperationId = "CreateScheduleDentist")]
    [SwaggerResponse(201, "The schedule dentist was created", typeof(ScheduleDentistResource))]
    [SwaggerResponse(400, "The schedule dentist was not created")]
    public async Task<ActionResult> CreateScheduleDentist([FromBody] CreateScheduleDentistResource resource)
    {
        var createScheduleDentistCommand =
            CreateScheduleDentistCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await scheduleDentistCommandService.Handle(createScheduleDentistCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetScheduleDentistById), new { result.id },
            ScheduleDentistResourceFromEntityAssembler.ToResourceFromEntity(result));
    }


    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a schedule dentist by id",
        Description = "Get a schedule dentist for a given identifier",
        OperationId = "GetScheduleDentistById")]
    [SwaggerResponse(200, "The schedule dentist was found", typeof(ScheduleDentistResource))]
    [SwaggerResponse(404, "The schedule dentist was not found")]
    public async Task<ActionResult> GetScheduleDentistById(int id)
    {
        var getScheduleDentistByIdQuery = new GetScheduleDentistByIdQuery(id);
        var result = await scheduleDentistQueryService.Handle(getScheduleDentistByIdQuery);
        if (result is null) return NotFound();
        var resource = ScheduleDentistResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the schedules dentists",
        Description = "Get all the schedules",
        OperationId = "GetAllSchedulesDentists")]
    [SwaggerResponse(200, "Schedules dentists were found", typeof(IEnumerable<ScheduleDentistResource>))]
    [SwaggerResponse(204, "Schedules dentists were not found")]
    public async Task<ActionResult> GetAllSchedulesDentists()
    {
        var getAllSchedulesDentistsQuery = new GetAllSchedulesDentistsQuery();
        var result = await scheduleDentistQueryService.Handle(getAllSchedulesDentistsQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(ScheduleDentistResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a schedule dentist",
        Description = "Delete a schedule dentist by its identifier",
        OperationId = "DeleteScheduleDentist")]
    [SwaggerResponse(200, "The schedule dentist was deleted successfully")]
    [SwaggerResponse(404, "The schedule dentist was not found")]
    public async Task<IActionResult> DeleteScheduleDentist(int id, DeleteScheduleDentistResource resource)
    {
        var getScheduleDentistByIdQuery = new GetScheduleDentistByIdQuery(id);
        var existingScheduleDentist = await scheduleDentistQueryService.Handle(getScheduleDentistByIdQuery);
        if (existingScheduleDentist is null) return NotFound("The specified schedule dentist could not be found.");

        var deleteScheduleDentistCommand =
            DeleteScheduleDentistCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await scheduleDentistCommandService.Handle(deleteScheduleDentistCommand, id);

        if (result is null)
            return BadRequest("The delete command could not be processed, check the provided identifier.");


        var deletedScheduleDentistResource = ScheduleDentistResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(deletedScheduleDentistResource);
    }
}