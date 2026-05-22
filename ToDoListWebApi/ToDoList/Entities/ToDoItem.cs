using MongoDB.Bson.Serialization.Attributes;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Domain.Entities;

public class ToDoItem
{
    public ToDoItem(string userId, string title, string description, Priority priority, DateTime deadline) 
    {
        UserId = userId;
        Title = title;
        Description = description;
        Priority = priority;
        Deadline = deadline;
    }
    
    [BsonId]
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateOnly CreationDate => DateOnly.FromDateTime(DateTime.Today);
}
