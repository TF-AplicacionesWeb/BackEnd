using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class SupportMessage
{
    public SupportMessage()
    {
    }
    
    public SupportMessage(int id, string name, string email, string description, int user_id)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.description = description;
        this.user_id = user_id;
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