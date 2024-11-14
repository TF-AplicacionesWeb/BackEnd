using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;

public class UpdateInventoryCommandFromResourceAssembler
{
    public static UpdateInventoryCommand ToCommandFromResource(UpdateInventoryResource resource) =>
        new UpdateInventoryCommand(resource.material_name, resource.quantity, resource.unit_price,
            resource.last_updated, resource.user_id);
}