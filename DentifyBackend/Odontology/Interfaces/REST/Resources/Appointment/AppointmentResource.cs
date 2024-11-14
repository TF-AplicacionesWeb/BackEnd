namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

public record AppointmentResource(int id, int dentist_dni, int user_id, DateTime appointment_date, string reason, bool completed, bool reminder_sent, int duration_minutes, int? payment_id, bool payment_status);