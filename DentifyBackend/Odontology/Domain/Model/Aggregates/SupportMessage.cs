using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class SupportMessage
{
    public SupportMessage()
    {
    }

    public SupportMessage(CreateSupportMessageCommand command)
    {
        name = command.name;
        email = command.email;
        description = command.description;
        user_id = command.user_id;
    }

    public int id { get; }
    public string name { get; private set; }
    public string email { get; private set; }
    public string description { get; private set; }
    public int user_id { get; private set; }
}