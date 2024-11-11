using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Dentify.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> FindByUsernameAsync(string username);
    
    Task<User?> FindByUsernameAndPasswordAsync(string username, string password);
    
}