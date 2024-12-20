using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.ScheduleDentist;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class ScheduleDentistCommandService(IScheduleDentistRepository scheduleDentistRepository, IUnitOfWork unitOfWork)
    : IScheduleDentistCommandService
{
    public async Task<ScheduleDentist?> Handle(CreateScheduleDentistCommand command)
    {
        var schedule =
            await scheduleDentistRepository.FindByDateAndTimeAsync(command.date, command.start_time, command.end_time);
        if (schedule != null) throw new InvalidOperationException("This schedule is already registered.");

        schedule = new ScheduleDentist(command);

        try
        {
            await scheduleDentistRepository.AddAsync(schedule);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the schedule. Please try again.", dbEx);
        }

        return schedule;
    }


    public async Task Handle(DeleteScheduleDentistCommand command)
    {
        var schedule = await scheduleDentistRepository.FindByIdAsync(command.id);

        if (schedule == null) throw new KeyNotFoundException($"Schedule dentist with ID {command.id} not found.");

        scheduleDentistRepository.Remove(schedule);
        
        await unitOfWork.CompleteAsync();

    }
}