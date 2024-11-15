using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Infrastructure.Repositories;

public class ScheduleDentistRepository(AppDbContext context)
    : BaseRepository<ScheduleDentist>(context), IScheduleDentistRepository
{
    public async Task<ScheduleDentist?> FindByDateAndTimeAsync(string date, string start_time, string end_time)
    {
        return await Context.Set<ScheduleDentist>().FirstOrDefaultAsync(
            scheduleDentist => scheduleDentist.date == date && scheduleDentist.start_time == start_time
                                                            && scheduleDentist.end_time == end_time);
    }
}