using System.Net.Mime;
using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Interfaces.REST.Resources;
using DentifyBackend.Odontology.Domain.Model.Queries.Payments;
using DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Dentify.Interfaces.REST;


[ApiController]
[Route("api/payments")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Payments")]
public class PaymentsController(
    IPaymentsCommandService paymentsCommandService,
    IPaymentsQueryService paymentsQueryService
    ): ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a payment",
        Description = "Create a payment with a given data",
        OperationId = "CreatePayments")]
    [SwaggerResponse(201, "The payment was created", typeof(PaymentsResource))]
    [SwaggerResponse(400, "The payment was not created")]
    public async Task<ActionResult> CreatePayments([FromBody] CreatePaymentsResource resource)
    {
        var createPaymentsCommand = CreatePaymentsCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await paymentsCommandService.Handle(createPaymentsCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetPaymentsById), new { id = result.id },
            PaymentsResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a payment by id",
        Description = "Get a payments for a given identifier",
        OperationId = "GetPaymentsById")]
    [SwaggerResponse(200, "The payment was found", typeof(DentistResource))]
    [SwaggerResponse(404, "The payment was not found")]
    public async Task<ActionResult> GetPaymentsById(int id)
    {
        var getPaymentsByIdQuery = new GetPaymentsByIdQuery(id);
        var result = await paymentsQueryService.Handle(getPaymentsByIdQuery);
        if (result is null) return NotFound();
        var resource = PaymentsResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}