using Mapster;
using TaskManagement.Application.Contracts;
using TaskManagement.Domain.DTOs.Request.Priority;

namespace TaskManagement.Application.Services.API.Priority;

public class PriorityService(IPriorityRepository priorityRepository):IPriorityService
{
    public async Task<List<Domain.Entity.Tasks.Priority>> GetAllAsync()
    {
        return await priorityRepository.GetAllAsync();
    }

    public async Task<Domain.Entity.Tasks.Priority?> GetByIdAsync(Guid id)
    {
        return await priorityRepository.GetByIdAsync(id);
    }

    public async Task<Domain.Entity.Tasks.Priority> CreateAsync(CreatePriorityDTO model)
    {
        var priority = model.Adapt<Domain.Entity.Tasks.Priority>();
        

        return await priorityRepository.CreateAsync(priority);
    }

    public async Task<Domain.Entity.Tasks.Priority?> UpdateAsync(Guid id, CreatePriorityDTO model)
    {
        var priority = await priorityRepository.GetByIdAsync(id);
        if (priority == null) return null;

        priority.Name = model.Name;
        priority.Slug = model.Slug;

        return await priorityRepository.UpdateAsync(priority);
    }

    public async Task<Domain.Entity.Tasks.Priority?> DeleteAsync(Guid id)
    {
        return await priorityRepository.DeleteAsync(id);
    }
}