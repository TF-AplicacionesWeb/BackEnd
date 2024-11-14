using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Odontology.Domain.Repositories;

public interface IScheduleDentistRepository : IBaseRepository<ScheduleDentist>
{
    Task<ScheduleDentist?> FindByDateAndTimeAsync(string date, string start_time, string end_time);
}