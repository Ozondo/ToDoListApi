using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ToDoListWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{
    static List<ToDoItem> _toDoList = [];

    [HttpGet("GetAll")]
    public List<ToDoItem> GetAll()
    {
        return _toDoList;
    }

    [HttpGet("Enumerable")]
    public List<ToDoItem> Enumerable(string name)
    {
        var neededElement = _toDoList.Where(x => x.Title.Contains(name));
        _toDoList.Add(new ToDoItem() { Title = "string"});
        var example = neededElement.ToList();

        return example;
    }

    [HttpPost("AddItem")]
    public string AddItem(ToDoItem item)
    {
        _toDoList.Add(item);

        return "Успешно добавлено";
    }

    [HttpDelete("DeleteItem")]
    public IActionResult DeleteItem(int id)
    {
        var deleteItem = _toDoList.FirstOrDefault(x => x.Id == id);

        if (deleteItem == null)
        {
            return NotFound("Дело не найдено");
        }

        _toDoList.Remove(deleteItem);

        return Ok("Успешно удалено");
    }
}
