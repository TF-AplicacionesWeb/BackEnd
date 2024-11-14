using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;
namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;

using DentifyBackend.Dentify.Domain.Model.Aggregates;


public class InventoryResourceFromEntityAssembler
{
    public static InventoryResource ToResourceFromEntity(Inventory entity) =>
        new InventoryResource(entity.id, entity.material_name, entity.quantity, entity.unit_price,
            entity.last_updated, entity.user_id);

}