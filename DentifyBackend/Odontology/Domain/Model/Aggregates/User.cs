using DentifyBackend.Odontology.Domain.Model.Commands.User;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class User
{
    protected User()
    {
        username = string.Empty;
        first_name = string.Empty;
        last_name = string.Empty;
        email = string.Empty;
        phone = string.Empty;
        register_date = DateTime.Now;
        company = string.Empty;
        password = string.Empty;
        trial = false;
    }

    public User(CreateUserCommand command)
    {
        username = command.username;
        first_name = command.first_name;
        last_name = command.last_name;
        email = command.email;
        phone = command.phone;
        register_date = DateTime.Now;
        company = command.company;
        password = command.password;
        trial = command.trial;
    }


    public int id { get; }
    public string username { get; private set; }
    public string first_name { get; private set; }
    public string last_name { get; private set; }
    public string email { get; private set; }
    public string phone { get; private set; }
    public DateTime register_date { get; private set; }
    public string company { get; private set; }
    public string password { get; private set; }

    public bool trial { get; private set; }

    public void SetAttributes(string username, string _firstName, string _lastName, string email, string phone,
        string company, string password, bool trial)
    {
        this.username = username;
        first_name = _firstName;
        last_name = _lastName;
        this.email = email;
        this.phone = phone;
        this.company = company;
        this.password = password;
        this.trial = trial;
    }
}