using DentifyBackend.Odontology.Domain.Model.Commands.ClinicalRecord;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class ClinicalRecord
{
    public ClinicalRecord()
    {
    }
    
    public ClinicalRecord(int id, string medical_history, string record_date, string diagnosis, string treatment, int user_id)
    {
        this.id = id;
        this.medical_history = medical_history;
        this.record_date = record_date;
        this.diagnosis = diagnosis;
        this.treatment = treatment;
        this.user_id = user_id;
    }

    public ClinicalRecord(CreateClinicalRecordCommand command)
    {
        id = command.id;
        medical_history = command.medical_history;
        record_date = command.record_date;
        diagnosis = command.diagnosis;
        treatment = command.treatment;
        user_id = command.user_id;
    }

    public int id { get; private set; }
    public string medical_history { get; private set; }
    public string record_date { get; private set; }
    public string diagnosis { get; private set; }
    public string treatment { get; private set; }
    public int user_id { get; private set; }
}