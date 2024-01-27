using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Dtos;
using TaskManagementSystem.API.Entity;

namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask(
        [FromBody] CreateTaskEntityDto taskEntityDto,
        [FromServices] IAppDbContext dbContext)
    {
        var created = dbContext.Tasks.Add(new TaskEntity
        {
            Id = new Guid(),
            Title = taskEntityDto.Title,
            Description = taskEntityDto.Description,
            DueDate = taskEntityDto.DueDate,
            Priority = taskEntityDto.Priority,
            State = taskEntityDto.State,
            Notes = taskEntityDto.Notes,
            CreatedAt = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = created.Entity.Id }, new GetTaskEntityDto(created.Entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(
        [FromServices] IAppDbContext dbContext,
        [FromQuery] string search,
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 25)
    {
        var tasksQuery = dbContext.Tasks.AsQueryable();

        if (string.IsNullOrWhiteSpace(search) == false)
            tasksQuery = tasksQuery.Where(u =>
                u.Title.ToLower().Contains(search.ToLower()));
        
        var tasks = await tasksQuery
            .Skip(limit * offset)
            .Take(limit)
            .Select(u => new GetTaskEntityDto(u))
            .ToListAsync();
        
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext)
    {
        var task = await dbContext.Tasks
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync();
        
        if (task is null)
            return NotFound();
        
        return Ok(new GetTaskEntityDto(task));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext,
        UpdateTaskEntityDto updateTaskEntity)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            return NotFound();
        
        task.Title = updateTaskEntity.Title;
        task.Description = updateTaskEntity.Description;
        task.DueDate = updateTaskEntity.DueDate;
        task.Priority = updateTaskEntity.Priority;
        task.State = updateTaskEntity.State;
        task.Notes = updateTaskEntity.Notes;

        await dbContext.SaveChangesAsync();

        return Ok();  
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(
        [FromRoute] Guid id,
        [FromServices] IAppDbContext dbContext)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            return NotFound();
        
        await dbContext.SaveChangesAsync();

        return Ok();
    }
}