using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands.SupportMessage;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class SupportMessageCommandService(ISupportMessageRepository supportMessageRepository, IUnitOfWork unitOfWork)
    : ISupportMessageCommandService
{
    public async Task<SupportMessage?> Handle(CreateSupportMessageCommand command)
    {
        var supportMessage = new SupportMessage(command);

        try
        {
            await supportMessageRepository.AddAsync(supportMessage);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error ocurred while saving the support message. Please, try again.", dbEx);
        }

        return supportMessage;
    }
}