namespace DentifyBackend.Odontology.Interfaces.REST.Resources.ScheduleDentist;

public record ScheduleDentistResource(
    int id,
    int dentist_id,
    string weekday,
    string start_time,
    string end_time,
    string date,
    int user_id);