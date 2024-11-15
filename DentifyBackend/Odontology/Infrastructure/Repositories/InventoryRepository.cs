using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Infrastructure.Repositories;

public class InventoryRepository(AppDbContext context) :
    BaseRepository<Inventory>(context), IInventoryRepository
{
    public async Task<Inventory?> FindByMaterialNameAsync(string material_name)
    {
        return await Context.Set<Inventory>().FirstOrDefaultAsync(
            inventory => inventory.material_name == material_name);
    }
}