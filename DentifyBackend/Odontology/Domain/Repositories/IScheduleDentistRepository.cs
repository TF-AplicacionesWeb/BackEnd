using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Dentify.Domain.Repositories;

public interface IScheduleDentistRepository : IBaseRepository<ScheduleDentist>
{
    Task<ScheduleDentist?> FindByDateAndTimeAsync(string date, string start_time, string end_time);
}