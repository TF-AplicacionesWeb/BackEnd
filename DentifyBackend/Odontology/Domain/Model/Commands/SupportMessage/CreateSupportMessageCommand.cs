namespace DentifyBackend.Dentify.Domain.Model.Commands.SupportMessage;

public record CreateSupportMessageCommand(string name, string email, string description, int user_id);