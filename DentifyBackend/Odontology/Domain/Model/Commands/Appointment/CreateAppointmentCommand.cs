using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DentifyBackend.Odontology.Domain.Model.Commands.Appointment;

public record CreateAppointmentCommand(int dentist_dni, int user_id, DateTime appointment_date, string reason, int duration_minutes, int payment_id);