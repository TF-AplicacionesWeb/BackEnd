using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;

public class PaymentsResourceFromEntityAssembler
{
    public static PaymentsResource ToResourceFromEntity(Domain.Model.Aggregates.Payments entity)
    {
        return new PaymentsResource(entity.id, entity.amount, entity.payment_date, entity.user_id);
    }
}