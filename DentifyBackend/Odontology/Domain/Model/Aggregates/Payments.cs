using DentifyBackend.Odontology.Domain.Model.Commands.Payments;

namespace DentifyBackend.Dentify.Domain.Model.Aggregates;

public class Payments
{
    public int id { get; set; }
    public float amount { get; set; }
    public DateTime payment_date { get; set; }
    public int user_id { get; set; }

    public Payments()
    {
        
    }
    public Payments(CreatePaymentsCommand command)
    {
        id = command.id;
        amount = command.amount;
        payment_date = command.payment_date;
        user_id = command.user_id;
    }
    
}