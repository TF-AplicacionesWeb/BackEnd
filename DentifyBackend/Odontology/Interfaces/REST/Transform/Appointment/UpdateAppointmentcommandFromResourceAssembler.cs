using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Appointment;

public static class UpdateAppointmentcommandFromResourceAssembler
{
    public static UpdateAppointmentCommand ToCommandFromResource(UpdateAppointmentResource resource)
    {
        return new UpdateAppointmentCommand(resource.appointment_date, resource.reason, resource.completed, resource.reminder_sent, resource.duration_minutes, resource.payment_id, resource.payment_status);
    }
}