namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

public record CreatePaymentsResource(int id,float amount, DateTime payment_date, int user_id);