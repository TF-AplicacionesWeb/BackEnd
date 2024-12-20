using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.ClinicalRecordService;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class ClinicalRecordComandService(IClinicalRecordRepository clinicalRecordRepository, IUnitOfWork unitOfWork)
    : IClinicalRecordCommandService
{
    public async Task<ClinicalRecord?> Handle(CreateClinicalRecordCommand command)
    {
        var clinicalRecord = new ClinicalRecord(command);

        try
        {
            await clinicalRecordRepository.AddAsync(clinicalRecord);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the clinical record. Please try again.", dbEx);
        }

        return clinicalRecord;
    }
}