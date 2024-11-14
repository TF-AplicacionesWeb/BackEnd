using DentifyBackend.Odontology.Domain.Model.Commands.Patient;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class Patient
{
    protected Patient()
    {
        id = 0;
        clinical_record_id = 0;
        first_name = "";
        last_name = "";
        email = "";
        age = 0;
        medical_history = "";
        birth_date = "";
        appointment_id = 0;
        user_id = 0;
    }

    public Patient(CreatePatientCommand command)
    {
        id = command.id;
        clinical_record_id = command.clinical_record_id;
        first_name = command.first_name;
        last_name = command.last_name;
        email = command.email;
        age = command.age;
        medical_history = command.medical_history;
        birth_date = command.birth_date;
        appointment_id = command.appointment_id;
        user_id = command.user_id;
    }

    public int id { get; }
    public int clinical_record_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public int age { get; set; }
    public string medical_history { get; set; }
    public string birth_date { get; set; }
    public int appointment_id { get; set; }
    public int user_id { get; set; }
    
}