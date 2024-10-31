namespace DentifyBackend.Dentify.Interfaces.REST.Resources;

public record CreateScheduleDentistResource(int dentist_id, string weekday, string start_time,
    string end_time, string date, int user_id);