using DentifyBackend.Odontology.Domain.Model.Queries.Appointment;

namespace DentifyBackend.Odontology.Domain.Services.Appointment;

public interface IAppointmentQueryService
{
    Task<IEnumerable<Model.Aggregates.Appointment>> Handle(GetAllAppointmentsQuery query);

    Task<Model.Aggregates.Appointment?> Handle(GetAppointmentByIdQuery query);
}