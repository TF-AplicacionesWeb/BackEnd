namespace DentifyBackend.Dentify.Interfaces.REST.Resources;

public record UpdateUserResource(string username, string first_name, string last_name,
    string email, string phone, string company, string password, bool trial);