using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Infrastructure.Repositories;

public class InventoryRepository(AppDbContext context):
    BaseRepository<Inventory>(context), IInventoryRepository
{
    public async Task<Inventory?> FindByMaterialNameAsync(string material_name)
    {
        return await Context.Set<Inventory>().FirstOrDefaultAsync(
            inventory => inventory.material_name == material_name);
    }
}