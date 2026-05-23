using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;
using ToDoListWebApi.ToDoList.Commands.AddItem;
using ToDoListWebApi.Users.IdAcess;

namespace ToDoListWebApi.ToDoList.Commands;

public class AddItemHandler
{
    private readonly IToDoListRepository _repository;
    private readonly ICurrentUserContext _currentUser;

    public AddItemHandler(IToDoListRepository repository, ICurrentUserContext currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public bool Handle(AddItemCommand command)
    {
        var userId = _currentUser.GetRequiredUserId();
        if (string.IsNullOrEmpty(command.Title.Replace(" ", "")))
        {
            return false;
        }
        if (command.Priority > Priority.High || command.Priority < Priority.Low)
        {
            return false;
        }

        if (command.Deadline < DateTime.Now)
        {
            return false;
        }

        var item = new ToDoItem(
            userId,
            command.Title,
            command.Description,
            command.Priority,
            command.Deadline
            );

        _repository.AddItem(item);

        return true;
    }
}