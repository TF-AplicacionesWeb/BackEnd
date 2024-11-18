using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;
using DentifyBackend.Odontology.Domain.Model.Queries.ScheduleDentist;
using DentifyBackend.Odontology.Domain.Services.ScheduleDentist;
using DentifyBackend.Odontology.Interfaces.REST.Controllers;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;


public class ScheduleDentistControllerUnitTest
{
    private readonly ScheduleDentistController _scheduleDentistController;
    private readonly Mock<IScheduleDentistCommandService> _mockScheduleDentistCommandService;
    private readonly Mock<IScheduleDentistQueryService> _mockScheduleDentistQueryService;
    
    public ScheduleDentistControllerUnitTest()
    {
        _mockScheduleDentistCommandService = new Mock<IScheduleDentistCommandService>();
        _mockScheduleDentistQueryService = new Mock<IScheduleDentistQueryService>();

        _scheduleDentistController =
            new ScheduleDentistController(_mockScheduleDentistCommandService.Object, _mockScheduleDentistQueryService.Object);

    }



    [Fact]
    public async Task CreateScheduleDentist_ReturnsCreatedResult_WhenScheduleDentistIsCreated()
    {
        var resource = new CreateScheduleDentistResource(1, "Monday", "15:00", "16:00", "17-11-14", 1);

        var createdScheduleDentist = new ScheduleDentist { dentist_id = 1, weekday = "Monday", start_time = "15:00", end_time = "16:00", date = "17-11-14", user_id = 1 };
        
        _mockScheduleDentistCommandService
            .Setup(service => service.Handle(It.IsAny<CreateScheduleDentistCommand>()))
            .ReturnsAsync(createdScheduleDentist);

        
        var result = await _scheduleDentistController.CreateScheduleDentist(resource);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.NotNull(actionResult.Value);
    }
    
    
    
    [Fact]
    public async Task CreateScheduleDentist_ReturnsBadRequest_WhenScheduleDentistCreationFails()
    {
        var resource = new CreateScheduleDentistResource(1, "Monday", "15:00", "16:00", "17-11-14", 1);
        
        _mockScheduleDentistCommandService
            .Setup(service => service.Handle(It.IsAny<CreateScheduleDentistCommand>()))
            .ReturnsAsync((ScheduleDentist)null);

        var result = await _scheduleDentistController.CreateScheduleDentist(resource);

        var actionResult = Assert.IsType<BadRequestResult>(result); 
        Assert.Equal(400, actionResult.StatusCode); 
    }
    
    
    [Fact]
    public async Task GetScheduleDentistById_ReturnsOk_WhenScheduleDentistIsFound()
    {
        var scheduleDentistId = 1;
        
        var scheduleDentist = new ScheduleDentist { dentist_id = 1, weekday = "Monday", start_time = "15:00", end_time = "16:00", date = "17-11-14", user_id = 1 };

        _mockScheduleDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetScheduleDentistByIdQuery>()))
            .ReturnsAsync(scheduleDentist);

        var result = await _scheduleDentistController.GetScheduleDentistById(1);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as ScheduleDentistResource;
        Assert.NotNull(returnValue);
        Assert.Equal(scheduleDentist.dentist_id, returnValue.dentist_id);
        Assert.Equal(scheduleDentist.weekday, returnValue.weekday);
        Assert.Equal(scheduleDentist.weekday, returnValue.weekday);
        Assert.Equal(scheduleDentist.start_time, returnValue.start_time);
        Assert.Equal(scheduleDentist.end_time, returnValue.end_time);
        Assert.Equal(scheduleDentist.date, returnValue.date);
        Assert.Equal(scheduleDentist.user_id, returnValue.user_id);
    }

    
    [Fact]
    public async Task GetScheduleDentistById_ReturnsNotFound_WhenScheduleDentistDoesNotExist()
    {
        int scheduleDentistId = 1;
        
        _mockScheduleDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetScheduleDentistByIdQuery>()))
            .ReturnsAsync((ScheduleDentist)null);

        var result = await _scheduleDentistController.GetScheduleDentistById(scheduleDentistId);

        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, actionResult.StatusCode);
    }
  
    
    
    [Fact]
    public async Task GetAllSheduleDentists_ReturnsOkResult_WhenScheduleDentistsAreFound()
    {
        var scheduleDentists = new List<ScheduleDentist>
        {
            new ScheduleDentist { dentist_id = 1, weekday = "Monday", start_time = "15:00", end_time = "16:00", date = "17-11-14", user_id = 1 },
            new ScheduleDentist { dentist_id = 2, weekday = "Wednesday", start_time = "11:00", end_time = "12:00", date = "20-11-14", user_id = 1 }
        };

        var scheduleDentistsResources = scheduleDentists.Select(d => new ScheduleDentistResource(
            d.id, d.dentist_id, d.weekday, d.start_time, d.end_time, d.date, d.user_id)).ToList();

        _mockScheduleDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllSchedulesDentistsQuery>()))
            .ReturnsAsync(scheduleDentists);

        var result = await _scheduleDentistController.GetAllSchedulesDentists();

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as IEnumerable<ScheduleDentistResource>;
        Assert.NotNull(returnValue);
        Assert.Equal(scheduleDentists.Count, returnValue.Count());
        Assert.Equal(scheduleDentistsResources.First().id, returnValue.First().id);
    }

    
    
    [Fact]
    public async Task GetAllScheduleDentists_ReturnsNoContent_WhenNoScheduleDentistsAreFound()
    {
        var emptyScheduleDentistsList = new List<ScheduleDentist>();

        _mockScheduleDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllSchedulesDentistsQuery>()))
            .ReturnsAsync(emptyScheduleDentistsList);

        var result = await _scheduleDentistController.GetAllSchedulesDentists();
        
        var actionResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, actionResult.StatusCode);
    }
    
    
    
    [Fact]
    public async Task DeleteScheduleDentist_ReturnsNoContent_WhenScheduleDentistIsDeletedSuccessfully()
    {
        _mockScheduleDentistCommandService
            .Setup(service => service.Handle(It.IsAny<DeleteScheduleDentistCommand>()))
            .Returns(Task.CompletedTask); 

        var result = await _scheduleDentistController.DeleteScheduleDentist(1);

        var actionResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, actionResult.StatusCode);
    }
}
