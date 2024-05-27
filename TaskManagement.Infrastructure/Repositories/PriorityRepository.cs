using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class PriorityRepository(AppDbContext context):IPriorityRepository
{
    public async Task<List<Domain.Entity.Tasks.Priority>> GetAllAsync()
    {
        return await context.Priorities.ToListAsync();
    }

    public async Task<Domain.Entity.Tasks.Priority?> GetByIdAsync(Guid id)
    {
        return await context.Priorities.FindAsync(id);
    }

    public async Task<Domain.Entity.Tasks.Priority> CreateAsync(Domain.Entity.Tasks.Priority priority)
    {
      
        await context.Priorities.AddAsync(priority);
        await context.SaveChangesAsync();
        return priority;
    }

   

    public async Task<Domain.Entity.Tasks.Priority?> UpdateAsync(Domain.Entity.Tasks.Priority priority)
    {
        var existingPriority = await context.Priorities.FindAsync(priority.Id);
        if (existingPriority == null) return null;

        existingPriority.Name = priority.Name;
        existingPriority.Slug = priority.Slug;

        await context.SaveChangesAsync();
        return existingPriority;
    }

    public async Task<Domain.Entity.Tasks.Priority?> DeleteAsync(Guid id)
    {
        var existingPriority = await context.Priorities.FindAsync(id);
        if (existingPriority == null) return null;

        context.Priorities.Remove(existingPriority);
        await context.SaveChangesAsync();
        return existingPriority;
    }
}