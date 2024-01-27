using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Entity;

namespace TaskManagementSystem.API.Data;

public class AppDbContext : DbContext, IAppDbContext
{

    public DbSet<TaskEntity> Tasks { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<TaskEntity>()
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();
        
        modelBuilder.Entity<TaskEntity>()
            .Property(e => e.Description)
            .HasMaxLength(500)
            .IsRequired();
        
        modelBuilder.Entity<TaskEntity>()
            .Property(e => e.DueDate)
            .HasDefaultValue(new DateTime(2023, 7, 10, 11, 29, 16, 314, DateTimeKind.Utc).AddTicks(6142));
        
        modelBuilder.Entity<TaskEntity>()
            .Property(e => e.Notes)
            .HasMaxLength(1000)
            .IsRequired();
        
        modelBuilder.Entity<TaskEntity>()
            .Property(e => e.CreatedAt)
            .HasDefaultValue(new DateTime(2023, 7, 10, 11, 29, 16, 314, DateTimeKind.Utc).AddTicks(6376));

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);
}