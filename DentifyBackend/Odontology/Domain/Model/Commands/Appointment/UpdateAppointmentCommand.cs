namespace DentifyBackend.Odontology.Domain.Model.Commands.Appointment;

public record UpdateAppointmentCommand(DateTime appointment_date, string reason, bool completed, bool reminder_sent, int duration_minutes, int payment_id, bool payment_status);