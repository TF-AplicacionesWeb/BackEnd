using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Infrastructure.Repositories;

public class DentistRepository(AppDbContext context)
    : BaseRepository<Dentist>(context), IDentistRepository
{
    public async Task<Dentist?> FindByFullNameAsync(string first_name, string last_name)
    {
        return await Context.Set<Dentist>().FirstOrDefaultAsync(
            dentist => dentist.first_name == first_name && dentist.last_name == last_name);
    }

    public async Task<Dentist?> FindByEmailAsync(string email)
    {
        return await Context.Set<Dentist>().FirstOrDefaultAsync(
            dentist => dentist.email == email);
    }

    public async Task<Dentist?> FindByPhoneAsync(string phone)
    {
        return await Context.Set<Dentist>().FirstOrDefaultAsync(
            dentist => dentist.phone == phone);
    }
}