using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.Appointment;
using DentifyBackend.Odontology.Domain.Services.Appointment;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Appointment;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Controllers;

[ApiController]
[Route("api/appointments")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("appointments")]
public class AppointmentController(IAppointmentCommandService commandService, IAppointmentQueryService queryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates an appointment",
        Description = "Creates an appointment",
        OperationId = "CreateAppointment")]
    [SwaggerResponse(201, "The user was created", typeof(PatientResource))]
    [SwaggerResponse(400, "The user was not created")]
    public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentResource resource)
    {
        var createAppCommand = CreateAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(createAppCommand);

        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetAppointmentById), new { result.id },
            AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the appointments",
        Description = "Get all the appointment",
        OperationId = "GetAllAppointments")]
    [SwaggerResponse(200, "appointment were found", typeof(IEnumerable<AppointmentResource>))]
    [SwaggerResponse(204, "appointment were not found")]
    public async Task<ActionResult> GetAllAppointments()
    {
        var getAllAppQuery = new GetAllAppointmentsQuery();
        var result = await queryService.Handle(getAllAppQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(AppointmentResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }


    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets an appointment by id",
        Description = "Gets an appointment for a given user identifier",
        OperationId = "GetAppointmentById")]
    [SwaggerResponse(200, "The appointment was found", typeof(AppointmentResource))]
    [SwaggerResponse(404, "The appointment was not found")]
    public async Task<ActionResult> GetAppointmentById(int id)
    {
        var query = new GetAppointmentByIdQuery(id);
        var result = await queryService.Handle(query);
        if (result is null) return NotFound();
        var resource = AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update an appointment",
        Description = "Update the description of an existing appointment",
        OperationId = "UpdateAppointment")]
    [SwaggerResponse(200, "The user was updated successfully", typeof(AppointmentResource))]
    [SwaggerResponse(404, "The user was not found")]
    public async Task<ActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentResource resource)
    {
        var query = new GetAppointmentByIdQuery(id);
        var existing = await queryService.Handle(query);
        if (existing is null) return NotFound("The specified patient could not be found.");

        var updateCommand = UpdateAppointmentcommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(updateCommand, id);

        if (result is null)
            return BadRequest("The update command could not be processed, check the provided data.");

        var updatedAppResource = AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(updatedAppResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete an appointment",
        Description = "Delete an appointment by its identifier",
        OperationId = "DeleteAppointment")]
    [SwaggerResponse(204, "The appointment was deleted successfully")]
    [SwaggerResponse(404, "The appointment was not found")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        var deleteResource = new DeleteAppointmentResource(id);

        var deleteCommand = DeleteAppointmentCommandFromResourceAssembler.ToCommandFromResource(deleteResource);

        await commandService.Handle(deleteCommand);

        return NoContent();
    }
}