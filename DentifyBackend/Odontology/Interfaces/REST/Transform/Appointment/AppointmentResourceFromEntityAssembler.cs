using DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Appointment;

public static class AppointmentResourceFromEntityAssembler
{
    public static AppointmentResource ToResourceFromEntity(Domain.Model.Aggregates.Appointment resource)
    {
        return new AppointmentResource(resource.id, resource.dentist_dni, resource.user_id, resource.appointment_date, resource.reason, resource.completed, resource.reminder_sent, resource.duration_minutes, resource.payment_id, resource.payment_status);
    }
}