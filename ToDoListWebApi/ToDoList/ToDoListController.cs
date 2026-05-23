using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.ToDoList.Commands;
using ToDoListWebApi.ToDoList.Commands.AddItem;
using ToDoListWebApi.ToDoList.Commands.DeleteItem;
using ToDoListWebApi.ToDoList.Queries.GetAllItems;
using ToDoListWebApi.ToDoList.Queries.GetAllItemsWithFilters;

namespace ToDoListWebApi.ToDoList;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly GetAllItemsHandler _getAllHandler;
    private readonly AddItemHandler _addItemHandler;
    private readonly DeleteItemHandler _deleteItemHandler;
    private readonly GetAllItemsWithFiltersHandler _getAllFiltersHandler;

    public ToDoListController(GetAllItemsHandler getAllHandler,
        AddItemHandler addItemHandler, DeleteItemHandler deleteItemHandler, 
        GetAllItemsWithFiltersHandler getAllFiltersHandler)
    {
        _getAllHandler = getAllHandler;
        _addItemHandler = addItemHandler;
        _deleteItemHandler = deleteItemHandler;
        _getAllFiltersHandler = getAllFiltersHandler;
        
    }

    [HttpGet("GetAll")]
    public async Task<List<ToDoItem>> GetAll()
    {
        return await _getAllHandler.Handle();
    }
    
    [HttpPost("AddItem")]
    public IActionResult AddItem(AddItemCommand item)
    {
        var result = _addItemHandler.Handle(item);
        return result ? Ok(item) : BadRequest();
    }

    [HttpDelete("DeleteItem")]
    public IActionResult DeleteItem(DeleteItemCommand item)
    {
        var result = _deleteItemHandler.Handle(item);
        
        return result ? Ok("Успешно удален") : NotFound("Не найдено дело");
    }
    
    [HttpPost("GetItemsWithFilters")]
    public List<ToDoItem> GetItemsWithFilters(GetAllItemsWithFiltersQuery query)
    {
        return _getAllFiltersHandler.Handle(query);
    }
}
