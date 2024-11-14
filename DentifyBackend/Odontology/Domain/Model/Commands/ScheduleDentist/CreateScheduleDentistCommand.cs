namespace DentifyBackend.Odontology.Domain.Model.Commands.ScheduleDentist;

public record CreateScheduleDentistCommand(
    int dentist_id,
    string weekday,
    string start_time,
    string end_time,
    string date,
    int user_id);