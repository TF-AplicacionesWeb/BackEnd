using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Model.Queries.Payments;
using DentifyBackend.Odontology.Domain.Services.Payments;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

public class PaymentsQueryService(IPaymentsRepository paymentsRepository)
    :IPaymentsQueryService
{

    public async Task<Payments?> Handle(GetPaymentsByIdQuery query)
    {
        return await paymentsRepository.FindByIdAsync(query.id);
    }
}