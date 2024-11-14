namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

public record PaymentsResource(int id, float amount, DateTime payment_date, int user_id);