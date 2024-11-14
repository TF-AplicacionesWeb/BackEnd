using DentifyBackend.Odontology.Domain.Model.Queries.Inventory;

namespace DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Dentify.Domain.Model.Aggregates;

public interface IIventoryQueryService
{
    Task<IEnumerable<Inventory>> Handle(GetAllInventoryQuery query);
    Task<Inventory?> Handle(GetInventoryByIdQuery query);
}