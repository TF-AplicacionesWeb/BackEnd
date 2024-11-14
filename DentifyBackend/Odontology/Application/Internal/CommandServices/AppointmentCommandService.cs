using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.Appointment;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Appointment;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class AppointmentCommandService(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
    : IAppointmentCommandService
{
    public async Task<Appointment> Handle(CreateAppointmentCommand command)
    {
       
        var appointment = new Appointment(command);
        
        try
        {
            await appointmentRepository.AddAsync(appointment);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the appointment. Please try again.", dbEx);
        }

        return appointment;
    }

    public async Task<Appointment> Handle(UpdateAppointmentCommand command, int id)
    {
        var existingAppoById = await appointmentRepository.FindByIdAsync(id);
        if (existingAppoById == null) throw new InvalidOperationException("An appointment with this ID does not exist.");
        
        existingAppoById.appointment_date = command.appointment_date;
        existingAppoById.reason = command.reason;
        existingAppoById.completed = command.completed;
        existingAppoById.reminder_sent = command.reminder_sent;
        existingAppoById.duration_minutes = command.duration_minutes;
        existingAppoById.payment_id  = command.payment_id;
        existingAppoById.payment_status = command.payment_status;
        
        try
        {
            appointmentRepository.Update(existingAppoById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the dentist. Please try again.", dbEx);
        }

        return existingAppoById;
    }

    public async Task Handle(DeleteAppointmentCommand command)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.id);

        if (appointment == null) throw new KeyNotFoundException($"Appointment with ID {command.id} not found.");
        
        appointmentRepository.Remove(appointment);
        
        await unitOfWork.CompleteAsync();
    }
}