using DentifyBackend.Odontology.Domain.Model.Commands.Payments;

namespace DentifyBackend.Odontology.Domain.Services.Payments;

public interface IPaymentsCommandService
{
    Task<Model.Aggregates.Payments?> Handle(CreatePaymentsCommand command);
    Task<Model.Aggregates.Payments?> Handle(UpdatePaymentsCommand command, int id);

    Task<Model.Aggregates.Payments?> Handle(DeletePaymentsCommand command, int id);
}