using System.Net.Mime;
using DentifyBackend.Odontology.Domain.Model.Queries.Inventory;
using DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;
using DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Odontology.Interfaces.REST.Controllers;

[ApiController]
[Route("api/inventory")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Inventory")]
public class InventoryController(
    IInventoryCommandService inventoryCommandService,
    IIventoryQueryService inventoryQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a product",
        Description = "Create a product with a given data",
        OperationId = "CreateInventory")]
    [SwaggerResponse(201, "The product was created", typeof(InventoryResource))]
    [SwaggerResponse(400, "The product was not created")]
    public async Task<ActionResult> CreateInventory([FromBody] CreateInventoryResource resource)
    {
        var createInventoryCommand = CreateInventoryCommandFromResourceAssembler.ToCommamdFromResource(resource);
        var result = await inventoryCommandService.Handle(createInventoryCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetInventoryById), new { result.id },
            InventoryResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a product by id",
        Description = "Get a podcut for a given identifier",
        OperationId = "GetInventoryById")]
    [SwaggerResponse(200, "The product was found", typeof(InventoryResource))]
    [SwaggerResponse(404, "The product was not found")]
    public async Task<ActionResult> GetInventoryById(int id)
    {
        var getInventoryByIdQuery = new GetInventoryByIdQuery(id);
        var result = await inventoryQueryService.Handle(getInventoryByIdQuery);
        if (result is null) return NotFound();
        var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the Inventory",
        Description = "Get all the products in Inventory",
        OperationId = "GetAllInventory")]
    [SwaggerResponse(200, "Products were found", typeof(IEnumerable<InventoryResource>))]
    [SwaggerResponse(204, "Products were not found")]
    public async Task<ActionResult> GetAllInventory()
    {
        var getAllInventoryQuery = new GetAllInventoryQuery();
        var result = await inventoryQueryService.Handle(getAllInventoryQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(InventoryResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a product",
        Description = "Update information about a product in inventory",
        OperationId = "UpdateInventory")]
    [SwaggerResponse(200, "The product was updated successfully", typeof(InventoryResource))]
    [SwaggerResponse(404, "The product was not found")]
    public async Task<ActionResult> UpdateInventory(int id, [FromBody] UpdateInventoryResource resource)
    {
        var getInventoryByIdQuery = new GetInventoryByIdQuery(id);
        var existingInventory = await inventoryQueryService.Handle(getInventoryByIdQuery);
        
        if (existingInventory is null)
            return NotFound("The specified product could not be found.");
        
        var updateInventoryCommand = UpdateInventoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await inventoryCommandService.Handle(updateInventoryCommand, id);
        
        if (result is null) 
            return BadRequest("The update command could not be processed, check the provided data.");
        
        
        var updatedInventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(result);
        
        return Ok(updatedInventoryResource);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a product",
        Description = "Delete a product by its identifier",
        OperationId = "DeleteInventory")]
    [SwaggerResponse(200, "The product was deleted successfully")]
    [SwaggerResponse(404, "The product was not found")]
    public async Task<IActionResult> DeleteInventory(int id)
    {
        var deleteInventoryResource = new DeleteteInvenotryResource(id);
        var deleteInventoryCommand =
            DeleteInventoryCommandFromResourceAssembler.ToCommandFromResource(deleteInventoryResource);
        await inventoryCommandService.Handle(deleteInventoryCommand);
        return NoContent();
    }
}