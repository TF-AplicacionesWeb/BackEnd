namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

public record UpdateAppointmentResource(DateTime appointment_date, string reason, bool completed, bool reminder_sent, int duration_minutes, int payment_id, bool payment_status);