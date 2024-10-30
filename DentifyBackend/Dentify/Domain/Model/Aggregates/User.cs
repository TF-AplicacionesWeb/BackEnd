using System.Runtime.InteropServices.JavaScript;
using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Model.Aggregates;

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
        
        
    }

    public User(CreateUserCommand command)
    {
        username = command.username;
        first_name = command.first_name;
        last_name = command.last_name;
        email = command.email;
        phone = command.phone;
        register_date = command.register_date;
        company = command.company;
        password = command.password;
    }
    
    
    public int id { get;}
    public string username { get; private set; }
    public string first_name { get; private set; }
    public string last_name { get; private set; }
    public string email { get; private set; }
    public string phone { get; private set; }
    public DateTime register_date { get; private set; }
    public string company { get; private set; }
    public string password { get; private set; }
}