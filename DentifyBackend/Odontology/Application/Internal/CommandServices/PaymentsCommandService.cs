using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Model.Commands.Payments;
using DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class PaymentsCommandService(IPaymentsRepository paymentsRepository, IUnitOfWork unitOfWork)
    :IPaymentsCommandService
{
    public async Task<Payments?> Handle(CreatePaymentsCommand command)
    {
        var existingPaymentsById = await paymentsRepository.FindByIdAsync(command.id);
        if (existingPaymentsById != null)
            throw new InvalidOperationException("A payments with this id already exists.");
        var payments = new Payments(command);
        try
        {
            await paymentsRepository.AddAsync(payments);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the payment. Please try again.", dbEx);
        }

        return payments;
    }
}