using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;

namespace DentifyBackend.Odontology.Domain.Services.Inventory;

public interface IInventoryCommandService
{
    Task<Model.Aggregates.Inventory?> Handle(CreateInventoryCommand command);
    Task<Model.Aggregates.Inventory?> Handle(UpdateInventoryCommand command, int id);
    Task Handle(DeleteInventoryCommand command);
}