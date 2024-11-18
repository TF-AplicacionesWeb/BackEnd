using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.Dentist;
using DentifyBackend.Odontology.Domain.Model.Queries.Dentist;
using DentifyBackend.Odontology.Domain.Services.Dentist;
using DentifyBackend.Odontology.Interfaces.REST.Controllers;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Dentist;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Tests;


public class DentistControllerUnitTest
{
    private readonly DentistController _dentistController;
    private readonly Mock<IDentistCommandService> _mockDentistCommandService;
    private readonly Mock<IDentistQueryService> _mockDentistQueryService;

    public DentistControllerUnitTest()
    {
        _mockDentistCommandService = new Mock<IDentistCommandService>();
        _mockDentistQueryService = new Mock<IDentistQueryService>();

        _dentistController = new DentistController(
            _mockDentistCommandService.Object,
            _mockDentistQueryService.Object
        );    
    }

    [Fact]
    public async Task CreateDentist_ReturnsCreatedResult_WhenDentistIsCreated()
    {
        var resource = new CreateDentistResource(1, "John", "Doe", "Orthodontics",
            10, "933578141", "john@gmail.com", 0, 1
        );

        var createdDentist = new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics",
            experience = 5, phone = "1234567890", email = "john.doe@example.com", total_appointments = 0, user_id = 1
        };

        _mockDentistCommandService
            .Setup(service => service.Handle(It.IsAny<CreateDentistCommand>()))
            .ReturnsAsync(createdDentist);

        
        var result = await _dentistController.CreateDentist(resource);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.NotNull(actionResult.Value);
    }
    
    
    
    [Fact]
    public async Task CreateDentist_ReturnsBadRequest_WhenDentistCreationFails()
    {
        var resource = new CreateDentistResource(1, "John", "Doe", "Orthodontics",
            10, "933578141", "john@gmail.com", 0, 1
        );
        
        _mockDentistCommandService
            .Setup(service => service.Handle(It.IsAny<CreateDentistCommand>()))
            .ReturnsAsync((Dentist)null);

        var result = await _dentistController.CreateDentist(resource);

        var actionResult = Assert.IsType<BadRequestResult>(result); 
        Assert.Equal(400, actionResult.StatusCode); 
    }

    
    [Fact]
    public async Task GetDentistById_ReturnsOkResult_WhenDentistExists()
    {
        int dentistId = 1;
        var dentist = new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics", experience = 5, phone = "1234567890", 
            email = "john.doe@example.com", total_appointments = 0, user_id = 1
        };

        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetDentistByIdQuery>()))
            .ReturnsAsync(dentist);

        var result = await _dentistController.GetDentistById(dentistId);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as DentistResource;
        Assert.NotNull(returnValue);
        Assert.Equal(dentist.id, returnValue.id);
        Assert.Equal(dentist.first_name, returnValue.first_name);
        Assert.Equal(dentist.last_name, returnValue.last_name);
        Assert.Equal(dentist.specialty, returnValue.specialty);
        Assert.Equal(dentist.experience, returnValue.experience);
        Assert.Equal(dentist.phone, returnValue.phone);
        Assert.Equal(dentist.email, returnValue.email);
        Assert.Equal(dentist.total_appointments, returnValue.total_appointments);
        Assert.Equal(dentist.user_id, returnValue.user_id);

    }
    
    
    [Fact]
    public async Task GetDentistById_ReturnsNotFound_WhenDentistDoesNotExist()
    {
        int dentistId = 1;
        
        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetDentistByIdQuery>()))
            .ReturnsAsync((Dentist)null);

        var result = await _dentistController.GetDentistById(dentistId);

        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, actionResult.StatusCode);
    }


    
    [Fact]
    public async Task GetAllDentists_ReturnsOkResult_WhenDentistsAreFound()
    {
        var dentists = new List<Dentist>
        {
            new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics", experience = 5,
                phone = "1234567890", email = "john.doe@example.com", total_appointments = 0, user_id = 1 },
            new Dentist { id = 2, first_name = "Jane", last_name = "Smith", specialty = "Pediatrics", experience = 7,
                phone = "0987654321", email = "jane.smith@example.com", total_appointments = 5, user_id = 2 }
        };

        var dentistResources = dentists.Select(d => new DentistResource(
            d.id, d.first_name, d.last_name, d.specialty, d.experience, d.phone, d.email, d.total_appointments, d.user_id
        )).ToList();

        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllDentistsQuery>()))
            .ReturnsAsync(dentists);

        var result = await _dentistController.GetAllDentists();

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as IEnumerable<DentistResource>;
        Assert.NotNull(returnValue);
        Assert.Equal(dentists.Count, returnValue.Count());
        Assert.Equal(dentistResources.First().id, returnValue.First().id);
    }
    
    
    [Fact]
    public async Task GetAllDentists_ReturnsNoContent_WhenNoDentistsAreFound()
    {
        var emptyDentistsList = new List<Dentist>();

        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllDentistsQuery>()))
            .ReturnsAsync(emptyDentistsList);

        var result = await _dentistController.GetAllDentists();
        
        var actionResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, actionResult.StatusCode);
    }

    
    
    
    [Fact]
    public async Task UpdateDentist_ReturnsOk_WhenDentistIsUpdatedSuccessfully()
    {
        var resource = new UpdateDentistResource("John", "Doe", "Orthodontics", 12, "936257418", "john.doe@updatedemail.com", 2, 1);
        var existingDentist = new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics", experience = 10, phone = "987654321", email = "john.doe@example.com", total_appointments = 1, user_id = 1};
        var updatedDentist = new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics", experience = 12, phone = "936257418", email = "john.doe@updatedemail.com", total_appointments = 2, user_id = 1};

        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetDentistByIdQuery>()))
            .ReturnsAsync(existingDentist);

        _mockDentistCommandService
            .Setup(service => service.Handle(It.IsAny<UpdateDentistCommand>(), It.IsAny<int>()))
            .ReturnsAsync(updatedDentist);

        var result = await _dentistController.UpdateDentist(1, resource);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
        var returnValue = Assert.IsType<DentistResource>(actionResult.Value);
        Assert.Equal(12, returnValue.experience);
        Assert.Equal("936257418", returnValue.phone);
        Assert.Equal(2, returnValue.total_appointments);
    }


    
    [Fact]
    public async Task UpdateDentist_ReturnsBadRequest_WhenUpdateFails()
    {
        var resource = new UpdateDentistResource("John", "Doe", "Orthodontics", 12, "936257418", "john.doe@updatedemail.com", 2, 1);
        var existingDentist = new Dentist { id = 1, first_name = "John", last_name = "Doe", specialty = "Orthodontics", experience = 10, phone = "987654321", email = "john.doe@example.com", total_appointments = 1, user_id = 1};

        _mockDentistQueryService
            .Setup(service => service.Handle(It.IsAny<GetDentistByIdQuery>()))
            .ReturnsAsync(existingDentist);

        _mockDentistCommandService
            .Setup(service => service.Handle(It.IsAny<UpdateDentistCommand>(), It.IsAny<int>()))
            .ReturnsAsync((Dentist)null);

        
        var result = await _dentistController.UpdateDentist(1, resource);
        
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, actionResult.StatusCode);
        Assert.Equal("The update command could not be processed, check the provided data.", actionResult.Value);
    }
    
    
    
    [Fact]
    public async Task DeleteDentist_ReturnsNoContent_WhenDentistIsDeletedSuccessfully()
    {
        _mockDentistCommandService
            .Setup(service => service.Handle(It.IsAny<DeleteDentistCommand>()))
            .Returns(Task.CompletedTask); 

        var result = await _dentistController.DeleteDentist(1);

        var actionResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, actionResult.StatusCode);
    }

}

