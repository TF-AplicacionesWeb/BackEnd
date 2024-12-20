namespace DentifyBackend.Odontology.Interfaces.REST.Resources.Inventory;

public record InventoryResource(
    int id,
    string material_name,
    int quantity,
    float unit_price,
    DateTime last_updated,
    int user_id);