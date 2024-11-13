using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace DentifyBackend.Dentify.Infrastructure.Repositories;

public class SupportMessageRepository(AppDbContext context)
    : BaseRepository<SupportMessage>(context), ISupportMessageRepository
{
    
}