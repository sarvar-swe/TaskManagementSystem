using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Entity;

namespace TaskManagementSystem.API.Data;

public interface IAppDbContext
{
    DbSet<TaskEntity> Tasks { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}