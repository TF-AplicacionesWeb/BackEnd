namespace DentifyBackend.Odontology.Domain.Model.Commands.Payments;

public record CreatePaymentsCommand(float amount, DateTime payment_date, int user_id);