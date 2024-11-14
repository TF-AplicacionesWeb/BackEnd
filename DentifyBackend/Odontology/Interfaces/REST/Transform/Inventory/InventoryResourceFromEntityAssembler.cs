using DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Inventory;

public class InventoryResourceFromEntityAssembler
{
    public static InventoryResource ToResourceFromEntity(Domain.Model.Aggregates.Inventory entity)
    {
        return new InventoryResource(entity.id, entity.material_name, entity.quantity, entity.unit_price,
            entity.last_updated, entity.user_id);
    }
}