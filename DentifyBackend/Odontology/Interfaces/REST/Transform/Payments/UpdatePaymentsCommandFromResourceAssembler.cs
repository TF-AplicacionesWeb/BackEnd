using DentifyBackend.Odontology.Domain.Model.Commands.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;

public class UpdatePaymentsCommandFromResourceAssembler
{

    public static UpdatePaymentsCommand ToCommandFromResource(UpdatePaymentsResource resource) =>
        new UpdatePaymentsCommand(resource.amount, resource.payment_date, resource.user_id);
}