using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Odontology.Domain.Model.Queries.Payments;

namespace DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Dentify.Domain.Model.Aggregates;
public interface IPaymentsQueryService
{
    Task<Payments?> Handle(GetPaymentsByIdQuery query);
}