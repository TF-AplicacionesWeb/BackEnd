using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.SupportMessage;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.SupportMessage;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

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