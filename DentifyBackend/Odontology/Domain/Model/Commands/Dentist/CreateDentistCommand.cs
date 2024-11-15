namespace DentifyBackend.Odontology.Domain.Model.Commands.Dentist;

public record CreateDentistCommand(
    int id,
    string first_name,
    string last_name,
    string specialty,
    int experience,
    string phone,
    string email,
    int total_appointments,
    int user_id);