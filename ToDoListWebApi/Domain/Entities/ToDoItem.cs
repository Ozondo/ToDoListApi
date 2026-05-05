using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Domain.Entities;

public class ToDoItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateOnly CreationDate => DateOnly.FromDateTime(DateTime.Today);
}
