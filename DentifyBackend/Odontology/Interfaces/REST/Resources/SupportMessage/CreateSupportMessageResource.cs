namespace DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;

public record CreateSupportMessageResource(
    string name,
    string email,
    string description,
    int user_id);