using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Model.Commands;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Dentify.Domain.Services;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class DentistCommandService(IDentistRepository dentistRepository, IUnitOfWork unitOfWork)
    : IDentistCommandService
{
    public async Task<Dentist?> Handle(CreateDentistCommand command)
    {
        var existingDentistById = await dentistRepository.FindByIdAsync(command.id);
        if (existingDentistById != null)  throw new InvalidOperationException("A dentist with this ID already exists.");

        var existingDentistByFullName = await dentistRepository.FindByFullNameAsync(command.first_name, command.last_name);
        if (existingDentistByFullName != null) throw new InvalidOperationException("A dentist with this full name already exists.");
        
        var existingDentistByEmail = await dentistRepository.FindByEmailAsync(command.email);
        if (existingDentistByEmail != null)  throw new InvalidOperationException("A dentist with this email already exists.");
        
        var existingDentistByPhone = await dentistRepository.FindByPhoneAsync(command.phone);
        if (existingDentistByPhone != null)  throw new InvalidOperationException("A dentist with this phone already exists.");
        
        var dentist = new Dentist(command);

        try
        {
            await dentistRepository.AddAsync(dentist);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the dentist. Please try again.", dbEx);
        }

        return dentist;
    }

    public async Task<Dentist?> Handle(UpdateDentistCommand command, int id)
    {
        var existingDentistById = await dentistRepository.FindByIdAsync(id);
        if (existingDentistById == null)  throw new InvalidOperationException("A dentist with this ID does not exist.");

        var existingDentistByFullName = await dentistRepository.FindByFullNameAsync(command.first_name, command.last_name);
        if (existingDentistByFullName != null && existingDentistByFullName.id != id) throw new InvalidOperationException("This full name already belongs to a dentist.");
        
        var existingDentistByEmail = await dentistRepository.FindByEmailAsync(command.email);
        if (existingDentistByEmail != null && existingDentistByEmail.id != id)  throw new InvalidOperationException("This email already belongs to a dentist.");
        
        var existingDentistByPhone = await dentistRepository.FindByPhoneAsync(command.phone);
        if (existingDentistByPhone != null && existingDentistByPhone.id != id)  throw new InvalidOperationException("This phone already belongs to a dentist.");

        existingDentistById.first_name = command.first_name;
        existingDentistById.last_name = command.last_name;
        existingDentistById.specialty = command.specialty;
        existingDentistById.experience = command.experience;
        existingDentistById.phone = command.phone;
        existingDentistById.email = command.email;
        existingDentistById.total_appointments = command.total_appointments;
        existingDentistById.user_id = command.user_id;

        try
        {
            dentistRepository.Update(existingDentistById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the dentist. Please try again.", dbEx);
        }

        return existingDentistById;
    }

    public async Task Handle(DeleteDentistCommand command)
    {
        var dentist = await dentistRepository.FindByIdAsync(command.id);
        
        if (dentist == null)
        {
            throw new KeyNotFoundException($"Dentist with ID {command.id} not found.");
        }

        // Eliminar el dentista
        dentistRepository.Remove(dentist);

        // Confirmar los cambios en la base de datos
        await unitOfWork.CompleteAsync();
    }

    
}