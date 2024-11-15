using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;

namespace DentifyBackend.Odontology.Domain.Services.Appointment;

public interface IAppointmentCommandService
{
    Task<Model.Aggregates.Appointment> Handle(CreateAppointmentCommand command);

    Task<Model.Aggregates.Appointment> Handle(UpdateAppointmentCommand command, int id);

    Task Handle(DeleteAppointmentCommand command);
}