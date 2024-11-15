using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace DentifyBackend.Odontology.Infrastructure.Repositories;

public class SupportMessageRepository(AppDbContext context)
    : BaseRepository<SupportMessage>(context), ISupportMessageRepository
{
}