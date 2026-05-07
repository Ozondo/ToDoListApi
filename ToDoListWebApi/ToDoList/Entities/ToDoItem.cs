using MongoDB.Bson.Serialization.Attributes;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Domain.Entities;

public class ToDoItem
{
    [BsonId]
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Low;
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateOnly CreationDate => DateOnly.FromDateTime(DateTime.Today);
}
