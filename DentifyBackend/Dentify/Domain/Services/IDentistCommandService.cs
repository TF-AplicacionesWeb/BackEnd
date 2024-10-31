using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IDentistCommandService
{
    Task<Dentist?> Handle(CreateDentistCommand command);

    Task<Dentist?> Handle(UpdateDentistCommand command, int id);

    Task<Dentist?> Handle(DeleteDentistCommand command, int id);
}