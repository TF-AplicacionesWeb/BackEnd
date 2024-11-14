using DentifyBackend.Odontology.Domain.Model.Queries.Payments;

namespace DentifyBackend.Odontology.Domain.Services.Payments;

public interface IPaymentsQueryService
{
    Task<IEnumerable<Model.Aggregates.Payments>> Handle(GetAllPaymentsQuery query);
    Task<Model.Aggregates.Payments?> Handle(GetPaymentsByIdQuery query);
}