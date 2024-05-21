using Application.Contracts;
using Application.DTOs.Request.Priority;
using Domain.Entity.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class PriorityRepository(AppDbContext context):IPriority
{
    public async Task<List<Priority>> GetAllAsync()
    {
        var priorities = await context.Priorities.ToListAsync();
       
        return priorities;
    }

    public async Task<Priority?> GetByIdAsync(Guid id)
    {
        var existingPriority = await context.Priorities.FindAsync(id);

        if (existingPriority == null)
        {
            return null;
            
        }
        return existingPriority;
    }

    public async Task<Priority> CreateAsync(CreatePriorityDTO model)
    {
        var priority = new Priority()
        {
            Name = model.Name,
            Slug = model.Slug
        };
        await context.Priorities.AddAsync(priority);
        await context.SaveChangesAsync();
        return priority;
    }

    public async Task<Priority?> UpdateAsync(Guid id, CreatePriorityDTO model)
    {
        var existingPriority = await context.Priorities.FindAsync(id);

        if (existingPriority == null)
        {
            return null;
            
        }
        
        existingPriority.Name = model.Name;
        existingPriority.Slug = model.Slug;
        
        await context.SaveChangesAsync();
        return existingPriority;
    }

    public async Task<Priority?> DeleteAsync(Guid id)
    {
        var existingPriority = await context.Priorities.FindAsync(id);

        if (existingPriority == null)
        {
            return null;
            
        }
        
        context.Priorities.Remove(existingPriority);
        await context.SaveChangesAsync();

        return existingPriority;
    }
}