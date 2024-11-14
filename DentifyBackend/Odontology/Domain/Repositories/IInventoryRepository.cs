using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Dentify.Domain.Repositories;

public interface IInventoryRepository: IBaseRepository<Inventory>
{
    Task<Inventory?> FindByMaterialNameAsync(string material_name);
}