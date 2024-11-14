using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;

namespace DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Dentify.Domain.Model.Aggregates;

public interface IInventoryCommandService
{
    Task<Inventory?> Handle(CreateInventoryCommand command);
    Task<Inventory?> Handle(UpdateInventoryCommand command, int id);
    Task<Inventory?> Handle(DeleteInventoryCommand command, int id);
}