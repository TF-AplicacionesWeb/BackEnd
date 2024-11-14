using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Odontology.Domain.Repositories;

public interface IDentistRepository : IBaseRepository<Dentist>
{
    Task<Dentist?> FindByFullNameAsync(string first_name, string last_name);

    Task<Dentist?> FindByEmailAsync(string email);

    Task<Dentist?> FindByPhoneAsync(string phone);
}