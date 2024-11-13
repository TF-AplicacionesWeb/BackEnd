using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;
using DentifyBackend.Dentify.Domain.Model.Aggregates;

public class PaymentsResourceFromEntityAssembler
{
    public static PaymentsResource ToResourceFromEntity(Payments entity) =>
        new PaymentsResource(entity.id, entity.amount, entity.payment_date, entity.user_id);
}