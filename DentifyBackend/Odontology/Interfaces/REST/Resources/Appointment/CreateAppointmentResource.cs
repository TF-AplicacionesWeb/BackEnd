namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

public record CreateAppointmentResource(int dentist_dni, int user_id, DateTime appointment_date, string reason, int duration_minutes);