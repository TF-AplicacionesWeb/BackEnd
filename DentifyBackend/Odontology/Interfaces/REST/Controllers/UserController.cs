using System.Net.Mime;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Dentify.Interfaces.REST.Resources;
using DentifyBackend.Dentify.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Dentify.Interfaces.REST;


[ApiController]
[Route("api/users")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("users")]
public class UserController(IUserCommandService userCommandService, IUserQueryService userQueryService): ControllerBase
{
    /// <summary>
    /// Creates a favorite source. 
    /// </summary>
    /// <param name="resource">CreateFavoriteSourceResource resource</param>
    /// <returns>
    /// A response as an action result containing the created favorite source, or bad request if the favorite source was not created.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a user",
        Description = "Creates a user",
        OperationId = "CreateFavoriteSource")]
    [SwaggerResponse(201, "The user was created", typeof(UserResource))]
    [SwaggerResponse(400, "The user was not created")]
    public async Task<ActionResult> CreateFavoriteSource([FromBody] CreateUserResource resource)
    {
        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(createUserCommand);
        if (result is null) return BadRequest();
        return CreatedAtAction(nameof(GetUserById), new { id = result.id }, UserResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all the users",
        Description = "Get all the users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(200, "Users were found", typeof(IEnumerable<UserResource>))]
    [SwaggerResponse(204, "Users were not found")]
    public async Task<ActionResult> GetAllUsers()
    {
        var getAllUserQuery = new GetAllUserQuery();
        var result = await userQueryService.Handle(getAllUserQuery);
        if (!result.Any()) return NoContent();
        var resources = result.Select(UserResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(resources);
    }
    
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a user by id",
        Description = "Gets a user for a given user identifier",
        OperationId = "GetUserById")]
    [SwaggerResponse(200, "The user was found", typeof(UserResource))]
    [SwaggerResponse(404, "The user was not found")]
    public async Task<ActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var result = await userQueryService.Handle(getUserByIdQuery);
        if (result is null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a user",
        Description = "Update the personal information of an existing user",
        OperationId = "UpdateUser")]
    [SwaggerResponse(200, "The user was updated successfully", typeof(UserResource))]
    [SwaggerResponse(404, "The user was not found")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserResource resource)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var existingUser = await userQueryService.Handle(getUserByIdQuery);
        if (existingUser is null)
        {
            return NotFound("The specified user could not be found.");
        }
        
        var updateUserCommand = UpdateUserCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(updateUserCommand, id);
        
        if (result is null) 
            return BadRequest("The update command could not be processed, check the provided data.");
        
        var updatedDentistResource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(updatedDentistResource);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a user",
        Description = "Delete a user by its identifier",
        OperationId = "DeleteDentist")]
    [SwaggerResponse(204, "The user was deleted successfully")]
    [SwaggerResponse(404, "The user was not found")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleteResource = new DeleteUserResource(id);
        
        var deleteCommand = DeleteUserCommandFromResourceAssembler.ToCommandFromResource(deleteResource);
        
        await userCommandService.Handle(deleteCommand);
        
        return NoContent();
    }
}