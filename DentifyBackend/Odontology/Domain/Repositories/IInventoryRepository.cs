using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Odontology.Domain.Repositories;

public interface IInventoryRepository : IBaseRepository<Inventory>
{
    Task<Inventory?> FindByMaterialNameAsync(string material_name);
}