using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;

namespace ToDoListWebApi.ToDoList;

public interface IToDoListRepository
{
    public Task<List<ToDoItem>> GetAll();
    public Task<bool> AddItem(ToDoItem item);
    public Task<bool> DeleteItem(string id);
    public Task<List<ToDoItem>> GetItemsWithFilters(FilterDefinition<ToDoItem> filter);
}