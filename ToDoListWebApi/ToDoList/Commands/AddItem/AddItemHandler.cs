using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;
using ToDoListWebApi.ToDoList.Commands.AddItem;

namespace ToDoListWebApi.ToDoList.Commands;

public class AddItemHandler
{
    private readonly IToDoListRepository _repository;

    public AddItemHandler(IToDoListRepository repository)
    {
        _repository = repository;
    }

    public bool Handle(AddItemCommand command)
    {
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
            command.UserId,
            command.Title,
            command.Description,
            command.Priority,
            command.Deadline
            );

        _repository.AddItem(item);

        return true;
    }
}