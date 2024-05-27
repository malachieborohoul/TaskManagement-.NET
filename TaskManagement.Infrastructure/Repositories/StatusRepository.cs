using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class StatusRepository(AppDbContext context):IStatusRepository
{
    public async Task<List<Domain.Entity.Tasks.Status>> GetAllAsync()
    {
        return await context.Status.ToListAsync();
    }

    public async Task<Domain.Entity.Tasks.Status?> GetByIdAsync(Guid id)
    {
        return await context.Status.FindAsync(id);
    }

    public async Task<Domain.Entity.Tasks.Status> CreateAsync(Domain.Entity.Tasks.Status status)
    {
        await context.Status.AddAsync(status);
        await context.SaveChangesAsync();
        return status;
    }

    public async Task<Domain.Entity.Tasks.Status?> UpdateAsync(Guid id, Domain.Entity.Tasks.Status status)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
        }

        existingStatus.Name = status.Name;
        existingStatus.Slug = status.Slug;

        await context.SaveChangesAsync();
        return existingStatus;
    }

    public async Task<Domain.Entity.Tasks.Status?> DeleteAsync(Guid id)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
        }

        context.Status.Remove(existingStatus);
        await context.SaveChangesAsync();

        return existingStatus;
    }
}