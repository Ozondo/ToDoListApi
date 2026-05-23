using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.ToDoList.Commands.AddItem;

public class AddItemCommand
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
}