using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class Appointment
{
    public int id { get; }
    public int dentist_dni { get; set; }
    public int user_id { get; set; }
    public DateTime appointment_date { get; set; }
    public string reason { get; set; }
    public bool completed { get; set; }
    public bool reminder_sent { get; set; }
    public int duration_minutes { get; set; }
    public int? payment_id { get; set; }
    public bool payment_status { get; set; }

    protected Appointment()
    {
        this.id = 0;
        this.dentist_dni = 0;
        this.user_id = 0;
        this.appointment_date = DateTime.Now;
        this.reason = "";
        this.completed = false;
        this.reminder_sent = false;
        this.duration_minutes = 0;
        this.payment_id = null;
        this.payment_status = false;

    }

    public Appointment(CreateAppointmentCommand command)
    {
        this.dentist_dni = command.dentist_dni;
        this.user_id = command.user_id;
        this.appointment_date = command.appointment_date;
        this.reason = command.reason;
        this.completed = false;
        this.reminder_sent = false;
        this.duration_minutes = command.duration_minutes;
        this.payment_id = null;
        this.payment_status = false;
    }
}