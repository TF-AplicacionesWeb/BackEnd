using DentifyBackend.Odontology.Domain.Model.Commands.Dentist;

namespace DentifyBackend.Odontology.Domain.Services.Dentist;

public interface IDentistCommandService
{
    Task<Model.Aggregates.Dentist?> Handle(CreateDentistCommand command);

    Task<Model.Aggregates.Dentist?> Handle(UpdateDentistCommand command, int id);

    Task Handle(DeleteDentistCommand command);
}