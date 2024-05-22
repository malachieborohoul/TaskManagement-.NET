
using TaskManagement.Domain.Entity.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs.Request.Status;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repos;

public class StatusRepository(AppDbContext context):IStatus
{
    public async Task<List<Status>> GetAllAsync()
    {
        var status = await context.Status.ToListAsync();
       
        return status;
    }

    public async Task<Status?> GetByIdAsync(Guid id)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }
        return existingStatus;
    }

    public async Task<Status> CreateAsync(CreateStatusDTO model)
    {

        var status = new Status()
        {
            Name = model.Name,
            Slug = model.Slug
        };
        
        await context.Status.AddAsync(status);
        await context.SaveChangesAsync();
        return status;
    }

    public async Task<Status?> UpdateAsync(Guid id, CreateStatusDTO model)
    {
        var existingStatus = await context.Status.FindAsync(id);

        if (existingStatus == null)
        {
            return null;
            
        }

        existingStatus.Name = model.Name;
        existingStatus.Slug = model.Slug;
        
        await context.SaveChangesAsync();
        return existingStatus;
        
        
    }

    public async Task<Status?> DeleteAsync(Guid id)
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