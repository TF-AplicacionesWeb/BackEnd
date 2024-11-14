using DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;

namespace DentifyBackend.Odontology.Domain.Services.ScheduleDentist;

public interface IScheduleDentistCommandService
{
    Task<Model.Aggregates.ScheduleDentist?> Handle(CreateScheduleDentistCommand command);

    Task<Model.Aggregates.ScheduleDentist?> Handle(DeleteScheduleDentistCommand command, int id);
}