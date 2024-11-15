using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.Patient;
using DentifyBackend.Odontology.Domain.Services.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Patient;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Patient;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Controllers;


[ApiController]
[Route("api/patients")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("patients")]
public class PatientController(IPatientCommandService userCommandService, IPatientQueryService userQueryService) : ControllerBase
{
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a patient",
        Description = "Creates a patient",
        OperationId = "CreatePatient")]
    [SwaggerResponse(201, "The patient was created", typeof(PatientResource))]
    [SwaggerResponse(400, "The patient was not created")]
    public async Task<ActionResult> CreatePatient([FromBody] CreatePatientResource resource)
    {
        var createPatientCommand = CreatePatientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(createPatientCommand);
        
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetPatientById), new { result.id },
            PatientResourceFromEntityAssembler.ToResourceFromEntity(result));
        
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the patients",
        Description = "Get all the patients",
        OperationId = "GetAllPatients")]
    [SwaggerResponse(200, "patients were found", typeof(IEnumerable<PatientResource>))]
    [SwaggerResponse(204, "patients were not found")]
    public async Task<ActionResult> GetAllPatients()
    {
        var getAllPatientQuery = new GetAllPatientsQuery();
        var result = await userQueryService.Handle(getAllPatientQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(PatientResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }


    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a patient by id",
        Description = "Gets a patient for a given user identifier",
        OperationId = "GetPatientById")]
    [SwaggerResponse(200, "The patient was found", typeof(PatientResource))]
    [SwaggerResponse(404, "The patient was not found")]
    public async Task<ActionResult> GetPatientById(int id)
    {
        var query = new GetPatientsByIdQuery(id);
        var result = await userQueryService.Handle(query);
        if (result is null) return NotFound();
        var resource = PatientResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a patient",
        Description = "Update the personal information of an existing patient",
        OperationId = "UpdatePatient")]
    [SwaggerResponse(200, "The patient was updated successfully", typeof(PatientResource))]
    [SwaggerResponse(404, "The patient was not found")]
    public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpdatePatientResource resource)
    {
        var query = new GetPatientsByIdQuery(id);
        var existing = await userQueryService.Handle(query);
        if (existing is null) return NotFound("The specified patient could not be found.");

        var updateCommand = UpdatePatientCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(updateCommand, id);

        if (result is null)
            return BadRequest("The update command could not be processed, check the provided data.");

        var updatedPatientResource = PatientResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(updatedPatientResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a patient",
        Description = "Delete a patient by its identifier",
        OperationId = "DeletePatient")]
    [SwaggerResponse(204, "The patient was deleted successfully")]
    [SwaggerResponse(404, "The patient was not found")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleteResource = new DeletePatientResource(id);

        var deleteCommand = DeletePatientCommandFromResourceAssembler.ToCommandFromResource(deleteResource);

        await userCommandService.Handle(deleteCommand);

        return NoContent();
    }
}