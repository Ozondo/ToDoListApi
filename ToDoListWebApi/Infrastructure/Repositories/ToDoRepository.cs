using ToDoListWebApi.Application.Interfaces;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Infrastructure.Repositories;

public class ToDoRepository: IToDoListRepository
{
    private static List<ToDoItem> _toDoList = new();
    
    public List<ToDoItem> GetAll()
    {
        return _toDoList;
    }

    public void AddItem(ToDoItem item)
    {
        _toDoList.Add(item);
    }

    public bool DeleteItem(Guid id)
    {
        var deleteItem = _toDoList.FirstOrDefault(x => x.Id == id);
        
        if (deleteItem == null)
        {
            return false;
        }
        
        _toDoList.Remove(deleteItem);
        
        return true;
    }

    public List<ToDoItem> GetItemsWithFilters(Priority? priority, bool? isCompleted, DateTime? deadline)
    {
        IEnumerable<ToDoItem> query = _toDoList;
        
        if (priority == null && isCompleted == null && deadline == null)
        {
            return _toDoList;
        }

        if (priority.HasValue)
        {
            query = query.Where(item => item.Priority == priority.Value);
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(item => item.IsCompleted == isCompleted.Value);
        }

        if (deadline.HasValue)
        {
            query = query.Where(item => item.Deadline == deadline.Value);
        }
        
        return query.ToList();
    }
}