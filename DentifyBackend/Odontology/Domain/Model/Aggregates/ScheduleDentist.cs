using DentifyBackend.Dentify.Domain.Model.Commands;

namespace DentifyBackend.Dentify.Domain.Model.Aggregates;

public class ScheduleDentist
{
    public int id { get; set; }
    public int dentist_id { get; set; }
    public string weekday { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string date { get; set; }
    
    public int user_id { get; set; }

    public ScheduleDentist(){}
    public ScheduleDentist(CreateScheduleDentistCommand command)
    {
        dentist_id = command.dentist_id;
        weekday = command.weekday;
        start_time = command.start_time;
        end_time = command.end_time;
        date = command.date;
        user_id = command.user_id;
    }
}