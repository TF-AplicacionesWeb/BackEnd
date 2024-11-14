using DentifyBackend.Odontology.Domain.Model.Commands.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;

public class CreatePaymentsCommandFromResourceAssembler
{
    public static CreatePaymentsCommand ToCommandFromResource(CreatePaymentsResource resource)
    {
        return new CreatePaymentsCommand(resource.id, resource.amount, resource.payment_date, resource.user_id);
    }
}