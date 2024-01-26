namespace TaskManagementSystem.API.Entity;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public State State { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}