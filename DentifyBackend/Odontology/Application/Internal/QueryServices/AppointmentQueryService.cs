using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Queries.Appointment;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Appointment;

namespace DentifyBackend.Odontology.Application.Internal.QueryServices;

public class AppointmentQueryService(IAppointmentRepository repository) : IAppointmentQueryService
{
    public async Task<IEnumerable<Appointment>> Handle(GetAllAppointmentsQuery query)
    {
        return await repository.ListAsync();

    }

    public async Task<Appointment?> Handle(GetAppointmentByIdQuery query)
    {
        return await repository.FindByIdAsync(query.id);
    }
}