using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.Inventory;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Inventory;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class InventoryQueryService(IInventoryRepository inventoryRepository)
    : IIventoryQueryService
{
    public async Task<IEnumerable<Inventory>> Handle(GetAllInventoryQuery query)
    {
        return await inventoryRepository.ListAsync();
    }

    public async Task<Inventory?> Handle(GetInventoryByIdQuery query)
    {
        return await inventoryRepository.FindByIdAsync(query.id);
    }
}