using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;

namespace DentifyBackend.Dentify.Domain.Services;

public interface IScheduleDentistQueryService
{
    Task<IEnumerable<ScheduleDentist>> Handle(GetAllSchedulesDentistsQuery query);
    Task<ScheduleDentist?> Handle(GetScheduleDentistByIdQuery query);

}