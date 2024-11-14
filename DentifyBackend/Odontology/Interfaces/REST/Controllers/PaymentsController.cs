using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.Payments;
using DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Controllers;

[ApiController]
[Route("api/payments")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Payments")]
public class PaymentsController(
    IPaymentsCommandService paymentsCommandService,
    IPaymentsQueryService paymentsQueryService
) : ControllerBase
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
        return CreatedAtAction(nameof(GetPaymentsById), new { result.id },
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


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the Payments",
        Description = "Get all the Payments",
        OperationId = "GetAllPayments")]
    [SwaggerResponse(200, "Payments were found", typeof(IEnumerable<PaymentsResource>))]
    [SwaggerResponse(204, "Payments were not found")]
    public async Task<ActionResult> GetAllPayments()
    {
        var getAllPaymentsQuery = new GetAllPaymentsQuery();
        var result = await paymentsQueryService.Handle(getAllPaymentsQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(PaymentsResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a payment",
        Description = "Update the information of an existing payment",
        OperationId = "UpdatePayment")]
    [SwaggerResponse(200, "The payment was updated successfully", typeof(PaymentsResource))]
    [SwaggerResponse(404, "The payment was not found")]
    public async Task<ActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentsResource resource)
    {
        var getPaymentsByIdQuery = new GetPaymentsByIdQuery(id);
        var existingPayment = await paymentsQueryService.Handle(getPaymentsByIdQuery);
        if (existingPayment is null) return NotFound("The specified payment could not be found.");

        var updatePaymentCommand = UpdatePaymentsCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await paymentsCommandService.Handle(updatePaymentCommand, id);

        if (result is null)
            return BadRequest("The update command could not be processed, check the provided data.");

        var updatedPaymentResource = PaymentsResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(updatedPaymentResource);
    }


    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a payment",
        Description = "Delete a payment by its identifier",
        OperationId = "DeletePayment")]
    [SwaggerResponse(200, "The payment was deleted successfully")]
    [SwaggerResponse(404, "The payment was not found")]
    public async Task<IActionResult> DeletePayment(int id, DeletePaymentsResource resource)
    {
        var getPaymentsByIdQuery = new GetPaymentsByIdQuery(id);
        var existingPayment = await paymentsQueryService.Handle(getPaymentsByIdQuery);
        if (existingPayment is null) return NotFound("The specified payment could not be found.");

        var deletePaymentCommand = DeletePaymentsCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await paymentsCommandService.Handle(deletePaymentCommand, id);

        if (result is null)
            return BadRequest("The delete command could not be processed, check the provided identifier.");

        var deletedPaymentResource = PaymentsResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(deletedPaymentResource);
    }
}