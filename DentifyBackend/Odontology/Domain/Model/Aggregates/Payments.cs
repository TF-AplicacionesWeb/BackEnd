using DentifyBackend.Odontology.Domain.Model.Commands.Payments;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class Payments
{
    public Payments()
    {
    }

    public Payments(CreatePaymentsCommand command)
    {
        amount = command.amount;
        payment_date = command.payment_date;
        user_id = command.user_id;
    }

    public int id { get; set; }
    public float amount { get; set; }
    public DateTime payment_date { get; set; }
    public int user_id { get; set; }
}