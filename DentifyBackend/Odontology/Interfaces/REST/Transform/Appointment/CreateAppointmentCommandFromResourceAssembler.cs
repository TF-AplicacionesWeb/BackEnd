using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Appointment;

public static class CreateAppointmentCommandFromResourceAssembler
{
    public static CreateAppointmentCommand ToCommandFromResource(CreateAppointmentResource resource)
    {
        return new CreateAppointmentCommand(resource.dentist_dni, resource.user_id, resource.appointment_date, resource.reason, resource.duration_minutes);
    }
}