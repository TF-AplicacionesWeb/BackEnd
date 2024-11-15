using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Shared.Domain.Repositories;

namespace DentifyBackend.Odontology.Domain.Repositories;

public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<Patient?> FindByIdAndEmail(int id, string email);
}