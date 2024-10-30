using DentifyBackend.Shared.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}