using DentifyBackend.Odontology.Domain.Model.Queries.ScheduleDentist;

namespace DentifyBackend.Odontology.Domain.Services.ScheduleDentist;

public interface IScheduleDentistQueryService
{
    Task<IEnumerable<Model.Aggregates.ScheduleDentist>> Handle(GetAllSchedulesDentistsQuery query);
    Task<Model.Aggregates.ScheduleDentist?> Handle(GetScheduleDentistByIdQuery query);
}