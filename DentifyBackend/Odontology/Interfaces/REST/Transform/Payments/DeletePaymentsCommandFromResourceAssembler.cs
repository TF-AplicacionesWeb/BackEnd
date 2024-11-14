using DentifyBackend.Odontology.Domain.Model.Commands.Payments;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Payments;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Payments;

public class DeletePaymentsCommandFromResourceAssembler
{
    public static DeletePaymentsCommand ToCommandFromResource(DeletePaymentsResource resource) =>
        new DeletePaymentsCommand(resource.id);
}