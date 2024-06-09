using Mapster;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.Status;

namespace TaskManagement.Application.Services.Status;

public class StatusService(IStatusRepository statusRepository):IStatusService
{
    public async Task<List<Domain.Entity.Tasks.Status>> GetAllAsync()
    {
        return await statusRepository.GetAllAsync();
    }

    public async Task<Domain.Entity.Tasks.Status?> GetByIdAsync(Guid id)
    {
        return await statusRepository.GetByIdAsync(id);
    }

    public async Task<Domain.Entity.Tasks.Status> CreateAsync(CreateStatusDTO model)
    {
        var status = model.Adapt<Domain.Entity.Tasks.Status>();

        return await statusRepository.CreateAsync(status);
    }

    public async Task<Domain.Entity.Tasks.Status?> UpdateAsync(Guid id, CreateStatusDTO model)
    {
        var existingStatus = await statusRepository.GetByIdAsync(id);

        if (existingStatus == null)
        {
            return null;
        }

        existingStatus.Name = model.Name;
        existingStatus.Slug = model.Slug;

        return await statusRepository.UpdateAsync(id, existingStatus);
    }

    public async Task<Domain.Entity.Tasks.Status?> DeleteAsync(Guid id)
    {
        return await statusRepository.DeleteAsync(id);
    }
}