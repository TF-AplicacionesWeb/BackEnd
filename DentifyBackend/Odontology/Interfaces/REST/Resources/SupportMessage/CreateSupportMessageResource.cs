namespace DentifyBackend.Dentify.Interfaces.REST.Resources.SupportMessage;

public record CreateSupportMessageResource(string name, string email, 
    string description, int user_id);