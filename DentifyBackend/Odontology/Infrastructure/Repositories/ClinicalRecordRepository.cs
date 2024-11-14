using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace DentifyBackend.Dentify.Infrastructure.Repositories;

public class ClinicalRecordRepository(AppDbContext context)
    : BaseRepository<ClinicalRecord>(context), IClinicalRecordRepository
{
    
}