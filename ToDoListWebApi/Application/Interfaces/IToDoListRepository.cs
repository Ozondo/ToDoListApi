using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Application.Interfaces;

public interface IToDoListRepository
{
    public List<ToDoItem> GetAll();
    public void AddItem(ToDoItem item);
    public bool DeleteItem(string id);
    public List<ToDoItem> GetItemsWithFilters(Priority? priority, bool? isCompleted, DateTime? deadline);
}