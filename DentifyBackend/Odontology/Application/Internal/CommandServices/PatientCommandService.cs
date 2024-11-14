using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.Patient;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Patient;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class PatientCommandService(IPatientRepository patientRepository, IUnitOfWork unitOfWork): IPatientCommandService
{
    public async Task<Patient?> Handle(CreatePatientCommand command)
    {
        var existingPatient = await patientRepository.FindByIdAsync(command.id);
        if (existingPatient is null) throw new InvalidOperationException($"Cannot find existing patient with id: {command.id}");
        var existingByIdAndEmail = await patientRepository.FindByIdAndEmail(command.id, command.email);
        if (existingByIdAndEmail != null) throw new InvalidOperationException($"Patient with id: {command.id} already exists");

        var patient = new Patient(command);
        
        try
        {
            await patientRepository.AddAsync(patient);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the dentist. Please try again.", dbEx);
        }

        return patient;
    }

    public async Task<Patient?> Handle(UpdatePatientCommand command, int id)
    {
        var existingPatientById = await patientRepository.FindByIdAsync(id);
        if (existingPatientById == null) throw new InvalidOperationException("A dentist with this ID does not exist.");
        
        existingPatientById.clinical_record_id = command.clinical_record_id;
        existingPatientById.first_name = command.first_name;
        existingPatientById.last_name = command.last_name;
        existingPatientById.email = command.email;
        existingPatientById.age = command.age;
        existingPatientById.medical_history = command.medical_history;
        existingPatientById.birth_date = command.birth_date;
        existingPatientById.appointment_id = command.appointment_id;
        existingPatientById.user_id = command.user_id;
        
        try
        {
            patientRepository.Update(existingPatientById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the dentist. Please try again.", dbEx);
        }

        return existingPatientById;
        
    }

    public async Task Handle(DeletePatientCommand command)
    {
        var patient = await patientRepository.FindByIdAsync(command.id);

        if (patient == null) throw new KeyNotFoundException($"Patient with ID {command.id} not found.");


        patientRepository.Remove(patient);


        await unitOfWork.CompleteAsync();
    }
}