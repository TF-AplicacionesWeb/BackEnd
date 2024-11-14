using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;

namespace DentifyBackend.Odontology.Domain.Model.Aggregates;

public class Inventory
{
    public Inventory()
    {
    }

    public Inventory(CreateInventoryCommand command)
    {
        id = command.id;
        material_name = command.material_name;
        quantity = command.quantity;
        unit_price = command.unit_price;
        last_updated = command.last_updated;
        user_id = command.user_id;
    }

    public int id { get; set; }
    public string material_name { get; set; }
    public int quantity { get; set; }
    public float unit_price { get; set; }
    public DateTime last_updated { get; set; }
    public int user_id { get; set; }
}