using System.Net.Mime;
using DentifyBackend.Dentify.Domain.Model.Queries.ClinicalRecord;
using DentifyBackend.Dentify.Domain.Services.ClinicalRecordService;
using DentifyBackend.Dentify.Interfaces.REST.Resources.ClinicalRecord;
using DentifyBackend.Dentify.Interfaces.REST.Transform.ClinicalRecordTransform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Dentify.Interfaces.REST;

[ApiController]
[Route("api/clinical_records")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Clinical Records")]
public class ClinicalRecordController(
    IClinicalRecordCommandService clinicalRecordCommandService,
    IClinicalRecordQueryService clinicalRecordQueryService): ControllerBase
{
    
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a clinical record",
        Description = "Create a clinical record with a given data",
        OperationId = "CreateClinicalRecord")]
    [SwaggerResponse(201, "The clinical record was created", typeof(ClinicalRecordResource))]
    [SwaggerResponse(400, "The clinical record was not created")]
    public async Task<ActionResult> CreateClinicalRecord([FromBody] CreateClinicalRecordResource resource)
    {
        var createClinicalRecordCommand = CreateClinicalRecordCommandFromResourceAssembler.toCommandFromResource(resource);
        var result = await clinicalRecordCommandService.Handle(createClinicalRecordCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetClinicalRecordById), new {id = result.id }, ClinicalRecordResourceFromEntityAssembler.toResourceFromEntity(result));
    }
    
    
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a clinical record by id",
        Description = "Get a clinical record for a given identifier",
        OperationId = "GetClinicalRecordById")]
    [SwaggerResponse(200, "The clinical record was found", typeof(ClinicalRecordResource))]
    [SwaggerResponse(404, "The clinical record was not found")]
    public async Task<ActionResult> GetClinicalRecordById(int id)
    {
        var getClinicalRecordByIdQuery = new GetClinicalRecordByIdQuery(id);
        var result = await clinicalRecordQueryService.Handle(getClinicalRecordByIdQuery);
        if (result is null) return NotFound();
        var resource = ClinicalRecordResourceFromEntityAssembler.toResourceFromEntity(result);
        return Ok(resource);
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the clinical records",
        Description = "Get all the clinical records",
        OperationId = "GetAllClinicalRecords")]
    [SwaggerResponse(200, "Clinical records were found", typeof(IEnumerable<ClinicalRecordResource>))]
    [SwaggerResponse(204, "Clinical records were not found")]
    public async Task<ActionResult> GetAllClinicalRecords()
    {
        var getAllClinicalRecordsQuery = new GetAllClinicalRecordsQuery();
        var result = await clinicalRecordQueryService.Handle(getAllClinicalRecordsQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(ClinicalRecordResourceFromEntityAssembler.toResourceFromEntity).ToList();
        return Ok(resources);
    }
}