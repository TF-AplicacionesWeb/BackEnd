using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Model.Queries.Inventory;
using DentifyBackend.Odontology.Domain.Services.Inventory;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

public class InventoryQueryService(IInventoryRepository inventoryRepository)
    :IIventoryQueryService
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