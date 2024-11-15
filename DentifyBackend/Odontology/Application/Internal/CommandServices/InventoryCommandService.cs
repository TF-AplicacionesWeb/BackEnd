using DentifyBackend.Odontology.Domain.Model.Aggregates;
using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;
using DentifyBackend.Odontology.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Odontology.Application.Internal.CommandServices;

public class InventoryCommandService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
    : IInventoryCommandService
{
    public async Task<Inventory?> Handle(CreateInventoryCommand command)
    {
      
        var existingInventoryByMaterialName = await inventoryRepository.FindByMaterialNameAsync(command.material_name);
        if (existingInventoryByMaterialName != null)
            throw new InvalidOperationException("A material with this name already exists.");

        var inventory = new Inventory(command);

        try
        {
            await inventoryRepository.AddAsync(inventory);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while saving the product. Please try again.", dbEx);
        }

        return inventory;
    }

    public async Task<Inventory?> Handle(UpdateInventoryCommand command, int id)
    {
        var existingInventoryById = await inventoryRepository.FindByIdAsync(id);
        if (existingInventoryById == null)
            throw new InvalidOperationException("A material with this ID does not exist.");
        var existingInventoryByMaterialName = await inventoryRepository.FindByMaterialNameAsync(command.material_name);
        if (existingInventoryByMaterialName != null && existingInventoryByMaterialName.id != id)
            throw new InvalidOperationException("This product already exists.");

        existingInventoryById.material_name = command.material_name;
        existingInventoryById.quantity = command.quantity;
        existingInventoryById.unit_price = command.unit_price;
        existingInventoryById.last_updated = new DateTime();
        existingInventoryById.user_id = command.user_id;

        try
        {
            inventoryRepository.Update(existingInventoryById);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while updating the product. Please try again.", dbEx);
        }

        return existingInventoryById;
    }

    public async Task Handle(DeleteInventoryCommand command)
    {
        var inventory = await inventoryRepository.FindByIdAsync(command.id);
        if(inventory==null) throw new KeyNotFoundException($"Product with ID {command.id} not found.");
        
        inventoryRepository.Remove((inventory));

        await unitOfWork.CompleteAsync();
    }
}