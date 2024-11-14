using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Infrastructure.Repositories;

public class PatientRepository(AppDbContext context)
    : BaseRepository<Patient>(context), IPatientRepository
{
    public async Task<Patient?> FindByIdAndEmail(int id, string email)
    {
        return await Context.Set<Patient>().FirstOrDefaultAsync(f => f.id == id && f.email == email);

    }
}