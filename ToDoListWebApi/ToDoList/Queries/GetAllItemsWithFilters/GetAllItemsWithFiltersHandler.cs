using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;

namespace ToDoListWebApi.ToDoList.Queries.GetAllItemsWithFilters;

public class GetAllItemsWithFiltersHandler
{
    private readonly IToDoListRepository _repository;

    public GetAllItemsWithFiltersHandler(IToDoListRepository repository)
    {
        _repository = repository;
    }

    public List<ToDoItem> Handle(GetAllItemsWithFiltersQuery command)
    {
        var filter = Builders<ToDoItem>.Filter.Empty;
        
        if (command.Priority == null && command.IsCompleted == null && command.DeadLine == null)
        {
            return _repository.GetItemsWithFilters(filter);
        }

        if (command.Priority.HasValue)
        {
            filter &= Builders<ToDoItem>.Filter.Eq(item => item.Priority, command.Priority.Value);
        }

        if (command.IsCompleted.HasValue)
        {
            filter &= Builders<ToDoItem>.Filter.Eq(item => item.IsCompleted, command.IsCompleted.Value);
        }

        if (command.DeadLine.HasValue)
        {
            filter &= Builders<ToDoItem>.Filter.Eq(item => item.Deadline, command.DeadLine.Value);
        }

        return _repository.GetItemsWithFilters(filter);
    }
}