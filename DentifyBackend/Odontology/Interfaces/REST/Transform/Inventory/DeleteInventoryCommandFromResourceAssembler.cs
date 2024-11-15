using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;

public class DeleteInventoryCommandFromResourceAssembler
{
    public static DeleteInventoryCommand ToCommandFromResource(DeleteteInvenotryResource resource)
    {
        return new DeleteInventoryCommand(resource.id);
    }
}