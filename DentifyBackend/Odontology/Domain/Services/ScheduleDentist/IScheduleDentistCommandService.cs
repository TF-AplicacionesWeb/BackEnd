using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IScheduleDentistCommandService
{
    Task<ScheduleDentist?> Handle(CreateScheduleDentistCommand command);
    
    Task<ScheduleDentist?> Handle(DeleteScheduleDentistCommand command, int id);
}