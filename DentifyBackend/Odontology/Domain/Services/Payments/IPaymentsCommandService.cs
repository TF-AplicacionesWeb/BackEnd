using DentifyBackend.Odontology.Domain.Model.Commands.Payments;

namespace DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Dentify.Domain.Model.Aggregates;
public interface IPaymentsCommandService
{
    Task<Payments?> Handle(CreatePaymentsCommand command);
    Task<Payments?> Handle(UpdatePaymentsCommand command, int id);
    
    Task<Payments?> Handle(DeletePaymentsCommand command, int id);
    
}