using TaskManagementSystem.API.Entity;

namespace TaskManagementSystem.API.Dtos;

public class GetTaskEntityDto
{
    public GetTaskEntityDto(TaskEntity entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        Description = entity.Description;
        DueDate = entity.DueDate;
        Priority = entity.Priority;
        State = entity.State;
        Notes = entity.Notes;
        CreatedAt = entity.CreatedAt;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public State State { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}