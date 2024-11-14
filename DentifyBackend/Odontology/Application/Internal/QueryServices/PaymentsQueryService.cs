using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.Payments;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Payments;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class PaymentsQueryService(IPaymentsRepository paymentsRepository)
    : IPaymentsQueryService
{
    public async Task<IEnumerable<Payments>> Handle(GetAllPaymentsQuery query)
    {
        return await paymentsRepository.ListAsync();
    }

    public async Task<Payments?> Handle(GetPaymentsByIdQuery query)
    {
        return await paymentsRepository.FindByIdAsync(query.id);
    }
}