using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;
using DentifyBackend.Odontology.Domain.Model.Queries.SupportMessage;
using DentifyBackend.Odontology.Domain.Services.SupportMessage;
using DentifyBackend.Odontology.Interfaces.REST.Controllers;
using DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class SupportMessageUnitTest
{
    private readonly SupportMessageController _supportMessageController;
    private readonly Mock<ISupportMessageCommandService> _mockSupportMessageCommandService;
    private readonly Mock<ISupportMessageQueryService> _mockSupportMessageQueryService;

    public SupportMessageUnitTest()
    {
        _mockSupportMessageCommandService = new Mock<ISupportMessageCommandService>();
        _mockSupportMessageQueryService = new Mock<ISupportMessageQueryService>();

        _supportMessageController =
            new SupportMessageController(_mockSupportMessageCommandService.Object, _mockSupportMessageQueryService.Object);

    }
    
    
    
    [Fact]
    public async Task CreateSupportMessage_ReturnsCreatedResult_WhenSupportMessageIsCreated()
    {
        var resource = new CreateSupportMessageResource("Joe", "joe@gmail.com", "Actualización inmediata", 1);

        var createdSupportMessage = new SupportMessage (1, "Joe", "joe@gmail.com", "Actualización inmediata",  1);
        
        _mockSupportMessageCommandService
            .Setup(service => service.Handle(It.IsAny<CreateSupportMessageCommand>()))
            .ReturnsAsync(createdSupportMessage);

        
        var result = await _supportMessageController.CreateSupportMessage(resource);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.NotNull(actionResult.Value);
    }
    
    
    [Fact]
    public async Task CreateSupportMessage_ReturnsBadRequest_WhenSupportMessageCreationFails()
    {
        var resource = new CreateSupportMessageResource("Joe", "joe@gmail.com", "Actualización inmediata", 1);
        
        _mockSupportMessageCommandService
            .Setup(service => service.Handle(It.IsAny<CreateSupportMessageCommand>()))
            .ReturnsAsync((SupportMessage)null);

        var result = await _supportMessageController.CreateSupportMessage(resource);

        var actionResult = Assert.IsType<BadRequestResult>(result); 
        Assert.Equal(400, actionResult.StatusCode); 
    }
    
    [Fact]
    public async Task GetSupportMessageById_ReturnsOk_WhenSupportMessageIsFound()
    {
        var supportMessageId = 1;
        
        var supportMessage = new SupportMessage (1, "Joe", "joe@gmail.com", "Actualización inmediata",  1);

        _mockSupportMessageQueryService
            .Setup(service => service.Handle(It.IsAny<GetSupportMessageByIdQuery>()))
            .ReturnsAsync(supportMessage);

        var result = await _supportMessageController.GetSupportMessageById(1);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as SupportMessageResource;
        Assert.NotNull(returnValue);
        Assert.Equal(supportMessage.id, returnValue.id);
        Assert.Equal(supportMessage.name, returnValue.name);
        Assert.Equal(supportMessage.email, returnValue.email);
        Assert.Equal(supportMessage.description, returnValue.description);
        Assert.Equal(supportMessage.user_id, returnValue.user_id);
    }

    [Fact]
    public async Task GetSupportMessageById_ReturnsNotFound_WhenSupportMessageDoesNotExist()
    {
        var supportMessageId = 1;
        
        _mockSupportMessageQueryService
            .Setup(service => service.Handle(It.IsAny<GetSupportMessageByIdQuery>()))
            .ReturnsAsync((SupportMessage)null);

        var result = await _supportMessageController.GetSupportMessageById(supportMessageId);

        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, actionResult.StatusCode);
    }

}
