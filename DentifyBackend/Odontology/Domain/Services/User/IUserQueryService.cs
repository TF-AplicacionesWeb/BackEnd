using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Queries;


namespace DentifyBackend.Dentify.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUserQuery query);
    Task<User?> Handle(GetUserByUsernameAndPasswordQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
}