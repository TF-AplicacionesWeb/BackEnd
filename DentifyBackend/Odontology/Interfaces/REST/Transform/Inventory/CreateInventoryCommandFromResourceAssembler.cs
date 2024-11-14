using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;

public class CreateInventoryCommandFromResourceAssembler
{
    public static CreateInventoryCommand ToCommamdFromResource(CreateInventoryResource resource)
    {
        return new CreateInventoryCommand(resource.id, resource.material_name, resource.quantity,
            resource.unit_price, resource.last_updated, resource.user_id);
    }
}