using TaskManagementSystem.API.Entity;

namespace TaskManagementSystem.API.Dtos;

public class CreateTaskEntityDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public State State { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}