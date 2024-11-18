using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;
using DentifyBackend.Odontology.Domain.Model.Queries.ClinicalRecord;
using DentifyBackend.Odontology.Domain.Services.ClinicalRecordService;
using DentifyBackend.Odontology.Interfaces.REST.Controllers;
using DentifyBackend.Odontology.Interfaces.REST.Resources.ClinicalRecord;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Tests;

public class ClinicalRecordUnitTest
{
    private readonly ClinicalRecordController _clinicalRecordController;
    private readonly Mock<IClinicalRecordCommandService> _mockClinicalRecordCommandService;
    private readonly Mock<IClinicalRecordQueryService> _mockClinicalRecordQueryService;

    public ClinicalRecordUnitTest()
    {
        _mockClinicalRecordCommandService = new Mock<IClinicalRecordCommandService>();
        _mockClinicalRecordQueryService = new Mock<IClinicalRecordQueryService>();
        _clinicalRecordController = new ClinicalRecordController(_mockClinicalRecordCommandService.Object,
            _mockClinicalRecordQueryService.Object);
    }
    
    
    [Fact]
    public async Task CreateClinicalRecord_ReturnsCreatedResult_WhenClinicalRecordIsCreated()
    {
        var resource = new CreateClinicalRecordResource(1, "None", "15-12-2023", "Orthodontics", "Orthodontics", 1);
        
        var createdClinicalRecord = new ClinicalRecord (1, "None", "15-12-2023", "Orthodontics", "Orthodontics", 1);
        
        _mockClinicalRecordCommandService
            .Setup(service => service.Handle(It.IsAny<CreateClinicalRecordCommand>()))
            .ReturnsAsync(createdClinicalRecord);

        
        var result = await _clinicalRecordController.CreateClinicalRecord(resource);

        var actionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(201, actionResult.StatusCode);
        Assert.NotNull(actionResult.Value);
    }
    
    
    [Fact]
    public async Task CreateClinicalRecord_ReturnsBadRequest_WhenClinicalRecordCreationFails()
    {
        var resource = new CreateClinicalRecordResource(1, "None", "15-12-2023", "Orthodontics", "Orthodontics", 1);
        
        _mockClinicalRecordCommandService
            .Setup(service => service.Handle(It.IsAny<CreateClinicalRecordCommand>()))
            .ReturnsAsync((ClinicalRecord)null);
        
        var result = await _clinicalRecordController.CreateClinicalRecord(resource);

        var actionResult = Assert.IsType<BadRequestResult>(result); 
        Assert.Equal(400, actionResult.StatusCode); 
    }
    
    
    [Fact]
    public async Task GetClinicalRecordById_ReturnsOk_WhenClinicalRecordIsFound()
    {
        var clinicalRecordId = 1;
        
        var clinicalRecord = new ClinicalRecord (1, "None", "15-12-2023", "Orthodontics", "Orthodontics", 1);

        _mockClinicalRecordQueryService
            .Setup(service => service.Handle(It.IsAny<GetClinicalRecordByIdQuery>()))
            .ReturnsAsync(clinicalRecord);

        var result = await _clinicalRecordController.GetClinicalRecordById(1);

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as ClinicalRecordResource;
        Assert.NotNull(returnValue);
        Assert.Equal(clinicalRecord.id, returnValue.id);
        Assert.Equal(clinicalRecord.medical_history, returnValue.medical_history);
        Assert.Equal(clinicalRecord.record_date, returnValue.record_date);
        Assert.Equal(clinicalRecord.diagnosis, returnValue.diagnosis);
        Assert.Equal(clinicalRecord.treatment, returnValue.treatment);
        Assert.Equal(clinicalRecord.user_id, returnValue.user_id);
    }
    
    [Fact]
    public async Task GetClinicalRecordById_ReturnsNotFound_WhenClinicalRecordDoesNotExist()
    {
        int clinicalRecordId = 1;
        
        _mockClinicalRecordQueryService
            .Setup(service => service.Handle(It.IsAny<GetClinicalRecordByIdQuery>()))
            .ReturnsAsync((ClinicalRecord)null);

        var result = await _clinicalRecordController.GetClinicalRecordById(clinicalRecordId);

        var actionResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(404, actionResult.StatusCode);
    }
    
    
    
    [Fact]
    public async Task GetAllClinicalRecords_ReturnsOkResult_WhenClinicalRecordsAreFound()
    {
        var clinicalRecords = new List<ClinicalRecord>
        {
            new ClinicalRecord (1, "None", "15-12-2023", "Orthodontics", "Orthodontics", 1),
            new ClinicalRecord (2, "None", "20-12-2023", "Orthodontics", "Orthodontics", 1)        
        };

        var clinicalRecordsResources = clinicalRecords.Select(d => new ClinicalRecordResource(
            d.id, d.medical_history, d.record_date, d.diagnosis, d.treatment, d.user_id)).ToList();

        _mockClinicalRecordQueryService
            .Setup(service => service.Handle(It.IsAny<GetAllClinicalRecordsQuery>()))
            .ReturnsAsync(clinicalRecords);

        var result = await _clinicalRecordController.GetAllClinicalRecords();

        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value as IEnumerable<ClinicalRecordResource>;
        Assert.NotNull(returnValue);
        Assert.Equal(clinicalRecords.Count, returnValue.Count());
        Assert.Equal(clinicalRecordsResources.First().id, returnValue.First().id);
    }
}
