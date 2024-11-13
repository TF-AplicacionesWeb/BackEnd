namespace DentifyBackend.Odontology.Domain.Model.Commands.Payments;

public record UpdatePaymentsCommand(float amount, DateTime payment_date, int user_id);