namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

public record UpdatePaymentsResource(float amount, DateTime payment_date, int user_id);