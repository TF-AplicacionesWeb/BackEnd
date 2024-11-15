using DentifyBackend.Odontology.Domain.Model.Queries.User;

namespace DentifyBackend.Odontology.Domain.Services.User;

public interface IUserQueryService
{
    Task<IEnumerable<Model.Aggregates.User>> Handle(GetAllUserQuery query);
    Task<Model.Aggregates.User?> Handle(GetUserByUsernameAndPasswordQuery query);
    Task<Model.Aggregates.User?> Handle(GetUserByIdQuery query);
}