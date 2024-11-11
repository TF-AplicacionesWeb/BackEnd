using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Dentify.Infrastructure.Repositories;

namespace DentifyBackend.Dentify.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    
    public async Task<IEnumerable<User>> Handle(GetAllUsersByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.username);
    }
    
    public async Task<User?> Handle(GetUserByUsernameAndPasswordQuery query)
    {
        return await userRepository.FindByUsernameAndPasswordAsync(query.username, query.password);
    }
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.id);
    }
}