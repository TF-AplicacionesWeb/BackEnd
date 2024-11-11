namespace DentifyBackend.Dentify.Domain.Model.Commands;

public record CreateScheduleDentistCommand(int dentist_id, string weekday, string start_time,
    string end_time, string date, int user_id);