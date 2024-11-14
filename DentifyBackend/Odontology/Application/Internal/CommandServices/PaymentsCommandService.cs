using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.Payments;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Payments;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class PaymentsCommandService(IPaymentsRepository paymentsRepository, IUnitOfWork unitOfWork)
    : IPaymentsCommandService
{
    public async Task<Payments?> Handle(CreatePaymentsCommand command)
    {
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

    public async Task<Payments?> Handle(UpdatePaymentsCommand command, int id)
    {
        var existingPaymentsById = await paymentsRepository.FindByIdAsync(id);
        if (existingPaymentsById == null)
            throw new InvalidOperationException("A payments with this id not exist");

        existingPaymentsById.amount = command.amount;
        existingPaymentsById.payment_date = new DateTime();
        existingPaymentsById.user_id = command.user_id;

        try
        {
            paymentsRepository.Update(existingPaymentsById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the Payment. Please try again.", dbEx);
        }

        return existingPaymentsById;
    }

    public async Task Handle(DeletePaymentsCommand command)
    {
        var payments = await paymentsRepository.FindByIdAsync(command.id);
        if(payments== null) throw new KeyNotFoundException($"Payment with ID {command.id} not found.");
        
        paymentsRepository.Remove(payments);

        await unitOfWork.CompleteAsync();

    }
}