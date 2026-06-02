using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Users.IdAcess;

namespace ToDoListWebApi.ToDoList.Queries.GetAllItemsWithFilters;

public class GetAllItemsWithFiltersHandler
{
    private readonly IToDoListRepository _repository;
    private readonly ICurrentUserContext _currentUser;
    

    public GetAllItemsWithFiltersHandler(IToDoListRepository repository, ICurrentUserContext currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<ToDoItem>> Handle(GetAllItemsWithFiltersQuery command)
    {
        var filter = Builders<ToDoItem>.Filter.Empty;
        filter &= Builders<ToDoItem>.Filter.Where(item => item.UserId == _currentUser.GetRequiredUserId());
        
        if (command.Priority == null && command.IsCompleted == null && command.DeadLine == null)
        {
            return await _repository.GetItemsWithFilters(filter);
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

        return await _repository.GetItemsWithFilters(filter);
    }
}