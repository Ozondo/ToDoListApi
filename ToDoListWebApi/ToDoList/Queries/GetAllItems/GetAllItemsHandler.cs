using ToDoListWebApi.Domain.Entities;

namespace ToDoListWebApi.ToDoList.Queries.GetAllItems;

public class GetAllItemsHandler
{
    private readonly IToDoListRepository _repository;

    public GetAllItemsHandler(IToDoListRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ToDoItem>> Handle()
    {
        return await _repository.GetAll();
    }
}