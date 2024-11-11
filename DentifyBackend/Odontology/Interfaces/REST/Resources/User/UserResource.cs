namespace DentifyBackend.Dentify.Interfaces.REST.Resources;

public record UserResource(int id, string username, string first_name, string last_name,
    string email, string phone, DateTime register_date, string company, string password, bool trial);