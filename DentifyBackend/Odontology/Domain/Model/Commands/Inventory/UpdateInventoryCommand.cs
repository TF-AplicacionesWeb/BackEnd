namespace DentifyBackend.Odontology.Domain.Model.Commands.Inventory;

public record UpdateInventoryCommand(string material_name, int quantity, float unit_price,
    DateTime last_updated, int user_id);