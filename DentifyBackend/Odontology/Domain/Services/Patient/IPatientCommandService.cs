using DentifyBackend.Odontology.Domain.Model.Commands.Patient;

namespace DentifyBackend.Odontology.Domain.Services.Patient;

public interface IPatientCommandService
{
    Task<Model.Aggregates.Patient?> Handle(CreatePatientCommand command);

    Task<Model.Aggregates.Patient?> Handle(UpdatePatientCommand command, int id);

    Task Handle(DeletePatientCommand command);
}