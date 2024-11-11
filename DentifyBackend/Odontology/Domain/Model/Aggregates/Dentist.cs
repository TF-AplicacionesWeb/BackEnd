using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Model.Aggregates;

public class Dentist
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string specialty { get; set; }
    public int experience { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public int total_appointments { get; set; }
    public int user_id { get; set; }
    public List<ScheduleDentist> schedules { get; set; }

    public Dentist()
    {
        schedules = new List<ScheduleDentist>();
    }
    public Dentist(CreateDentistCommand command)
    {
        id = command.id;
        first_name = command.first_name;
        last_name = command.last_name;
        specialty = command.specialty;
        experience = command.experience;
        phone = command.phone;
        email = command.email;
        total_appointments = command.total_appointments;
        user_id = command.user_id;
        schedules = new List<ScheduleDentist>();
    }
}