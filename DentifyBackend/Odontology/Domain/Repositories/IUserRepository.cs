using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Dentify.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAndPasswordAsync(string username, string password);
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByPhone(string phone);
    
}