using DentifyBackend.Odontology.Domain.Model.Queries.Inventory;

namespace DentifyBackend.Odontology.Domain.Services.Inventory;

public interface IIventoryQueryService
{
    Task<IEnumerable<Model.Aggregates.Inventory>> Handle(GetAllInventoryQuery query);
    Task<Model.Aggregates.Inventory?> Handle(GetInventoryByIdQuery query);
}