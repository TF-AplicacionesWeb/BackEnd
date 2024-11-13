using DentifyBackend.Dentify.Domain.Model.Commands.ClinicalRecord;

namespace DentifyBackend.Dentify.Domain.Model.Aggregates;

public class ClinicalRecord
{
    public int id { get; private set; }
    public string medical_history { get; private set; }
    public string record_date { get; private set; }
    public string diagnosis { get; private set; }
    public string treatment { get; private set; }
    public int user_id { get; private set; }
    
    public ClinicalRecord() {}

    public ClinicalRecord(CreateClinicalRecordCommand command)
    {
        id = command.id;
        medical_history = command.medical_history;
        record_date = command.record_date;
        diagnosis = command.diagnosis;
        treatment = command.treatment;
        user_id = command.user_id;
    }
}