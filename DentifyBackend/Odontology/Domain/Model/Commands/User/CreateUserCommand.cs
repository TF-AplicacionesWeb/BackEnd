namespace DentifyBackend.Odontology.Domain.Model.Commands.User;

public record CreateUserCommand(
    string username,
    string first_name,
    string last_name,
    string email,
    string phone,
    string company,
    string password,
    bool trial);