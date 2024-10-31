namespace DentifyBackend.Dentify.Domain.Model.Commands;

public record UpdateDentistCommand(string first_name, string last_name,
    string specialty, int experience, string phone, string email,
    int total_appointments, int user_id);