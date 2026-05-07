using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;

namespace ToDoListWebApi.ToDoList;

public interface IToDoListRepository
{
    public Task<List<ToDoItem>> GetAll();
    public bool AddItem(ToDoItem item);
    public bool DeleteItem(string id);
    public List<ToDoItem> GetItemsWithFilters(FilterDefinition<ToDoItem> filter);
}