using System.Net.Mime;
using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Dentify.Interfaces.REST.Resources;
using DentifyBackend.Dentify.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DentifyBackend.Dentify.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Users")]
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
    
    /// <summary>
    /// FALTA CAMBIAR A USER
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<ActionResult> GetAllUsersByUsername(string username)
    {
        var getAllUsersByUsernameQuery = new GetAllUsersByUsernameQuery(username);
        var result = await userQueryService.Handle(getAllUsersByUsernameQuery);
        var resources = result.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    private async Task<ActionResult> GetUserByUsernameAndPassword(string username, string password)
    {
        var getUserByUsernameAndPasswordQuery = new GetUserByUsernameAndPasswordQuery(username, password);
        var result = await userQueryService.Handle(getUserByUsernameAndPasswordQuery);
        if (result is null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets a user according to parameters",
        Description = "Gets a user for given parameters",
        OperationId = "GetUserFromQuery")]
    [SwaggerResponse(200, "Result(s) was/were found", typeof(UserResource))]
    public async Task<ActionResult> GetFavoriteSourceFromQuery([FromQuery] string username, [FromQuery] string password = "")
    {
        return string.IsNullOrEmpty(password)
            ? await GetAllUsersByUsername(username)
            : await GetUserByUsernameAndPassword(username, password);
    }
    
    
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Gets a user by id",
        Description = "Gets a user for a given user identifier",
        OperationId = "GetUserById")]
    [SwaggerResponse(200, "The favorite source was found", typeof(UserResource))]
    public async Task<ActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var result = await userQueryService.Handle(getUserByIdQuery);
        if (result is null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}