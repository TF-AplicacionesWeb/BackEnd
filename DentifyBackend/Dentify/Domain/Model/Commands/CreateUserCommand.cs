namespace DentifyBackend.Dentify.Domain.Model.Commands;

public record CreateUserCommand(string username, string first_name, string last_name,
    string email, string phone, DateTime register_date, string company, string password);