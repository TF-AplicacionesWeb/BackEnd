using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using DentifyBackend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    
    public async Task<IEnumerable<User>> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().Where(f => f.username == username).ToListAsync();
    }
    
    
    public async Task<User?> FindByUsernameAndPasswordAsync(string username, string password)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(f => f.username == username && f.password == password);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(
            user => user.email == email);
    }

    public async Task<User?> FindByPhone(string phone)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(
            user => user.phone == phone);
    }
}