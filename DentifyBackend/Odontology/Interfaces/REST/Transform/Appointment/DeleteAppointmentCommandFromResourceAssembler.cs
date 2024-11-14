using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;
using DentifyBackend.Odontology.Interfaces.REST.Resources.Appointment;

namespace DentifyBackend.Odontology.Interfaces.REST.Transform.Appointment;

public class DeleteAppointmentCommandFromResourceAssembler
{
    public static DeleteAppointmentCommand ToCommandFromResource(DeleteAppointmentResource resource)
    {
        return new DeleteAppointmentCommand(resource.id);
    }
}