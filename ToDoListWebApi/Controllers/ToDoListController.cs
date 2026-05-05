using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Application.Interfaces;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly IToDoListRepository _repository;

    public ToDoListController(IToDoListRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetAll")]
    public List<ToDoItem> GetAll()
    {
        return _repository.GetAll();
    }
    
    [HttpPost("AddItem")]
    public IActionResult AddItem(ToDoItem item)
    {
        _repository.AddItem(item);

        return Ok("Успешно добавлено");
    }

    [HttpDelete("DeleteItem")]
    public IActionResult DeleteItem(string id)
    {
        var result = _repository.DeleteItem(id);
        
        return result ? Ok("Успешно удален") : NotFound("Не найдено дело");
    }
    
    [HttpPost("GetItemsWithFilters")]
    public List<ToDoItem> GetItemsWithFilters(Priority? priority, bool? isCompleted, DateTime? deadline)
    {
        return _repository.GetItemsWithFilters(priority, isCompleted, deadline);
    }
}
