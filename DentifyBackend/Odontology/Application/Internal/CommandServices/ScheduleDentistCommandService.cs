using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class ScheduleDentistCommandService(IScheduleDentistRepository scheduleDentistRepository, IUnitOfWork unitOfWork)
    : IScheduleDentistCommandService
{
    public async Task<ScheduleDentist?> Handle(CreateScheduleDentistCommand command)
    {
        var schedule = await scheduleDentistRepository.FindByDateAndTimeAsync(command.date, command.start_time, command.end_time);
        if(schedule != null) throw new InvalidOperationException("This schedule is already registered.");

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
    
    
    public async Task<ScheduleDentist?> Handle(DeleteScheduleDentistCommand command, int id)
    {
        var schedule = await scheduleDentistRepository.FindByIdAsync(id);

        if (schedule == null) throw new InvalidOperationException("Schedule with this ID  does not exist");

        try
        {
            scheduleDentistRepository.Remove(schedule);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while deleting the schedule. Please try again.", dbEx);
        }

        return schedule;
    }

}