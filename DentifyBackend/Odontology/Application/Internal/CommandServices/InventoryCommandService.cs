using DentifyBackend.Dentify.Domain.Model.Aggregates;
using DentifyBackend.Dentify.Domain.Repositories;
using DentifyBackend.Odontology.Domain.Model.Commands.Inventory;
using DentifyBackend.Odontology.Domain.Services.Inventory;
using DentifyBackend.Shared.Domain.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace DentifyBackend.Dentify.Application.Internal.CommandServices;

public class InventoryCommandService(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork): IInventoryCommandService
{
    public async Task<Inventory?> Handle(CreateInventoryCommand command)
    {
        var existingInventoryById = await inventoryRepository.FindByIdAsync(command.id);
        if (existingInventoryById != null)
            throw new InvalidOperationException("A material with this ID already exists.");
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

    public async Task<Inventory?> Handle(DeleteInventoryCommand command, int id)
    {
        var inventory = await inventoryRepository.FindByIdAsync(id);
        if (inventory == null) throw new InvalidOperationException("Product with this ID does not exist");

        try
        {
            inventoryRepository.Remove(inventory);
            await unitOfWork.CompleteAsync();
        }
        catch (DbUpdateException dbEx)
        {
            throw new Exception("An error occurred while deleting the product. Please try again.", dbEx);
        }

        return inventory;
    }
    
    
}