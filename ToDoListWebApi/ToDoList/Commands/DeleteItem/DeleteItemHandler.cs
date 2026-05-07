namespace ToDoListWebApi.ToDoList.Commands.DeleteItem;

public class DeleteItemHandler
{
    private readonly IToDoListRepository _repository;

    public DeleteItemHandler(IToDoListRepository repository)
    {
        _repository = repository;
    }

    public bool Handle(DeleteItemCommand command)
    {
        var result = _repository.DeleteItem(command.Id);
        
        return result;
    }
}