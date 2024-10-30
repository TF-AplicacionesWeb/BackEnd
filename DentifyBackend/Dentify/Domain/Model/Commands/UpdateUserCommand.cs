namespace DentifyBackend.Dentify.Domain.Model.Commands;

public record UpdateUserCommand(string username, string first_name, string last_name,
    string email, string phone, string company, string password);