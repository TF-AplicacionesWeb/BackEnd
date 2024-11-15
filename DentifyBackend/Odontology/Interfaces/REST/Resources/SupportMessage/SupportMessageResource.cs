namespace DentifyBackend.Odontology.Interfaces.REST.Resources.SupportMessage;

public record SupportMessageResource(
    int id,
    string name,
    string email,
    string description,
    int user_id);