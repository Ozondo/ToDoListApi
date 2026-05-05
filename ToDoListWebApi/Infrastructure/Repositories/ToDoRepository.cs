using MongoDB.Driver;
using ToDoListWebApi.Application.Interfaces;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Domain.Enums;

namespace ToDoListWebApi.Infrastructure.Repositories;

public class ToDoRepository: IToDoListRepository
{
    private readonly IMongoCollection<ToDoItem> _toDoCollection;
    
    public ToDoRepository(MongoDbSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

        _toDoCollection = mongoDatabase.GetCollection<ToDoItem>(mongoSettings.ToDoCollectionName);
    }
    
    public List<ToDoItem> GetAll()
    {
        return _toDoCollection.Find(_ => true).ToList();
    }

    public void AddItem(ToDoItem item)
    {
        _toDoCollection.InsertOne(item);
    }

    public bool DeleteItem(string id)
    {
        var deleteItem = _toDoCollection.Find(item => item.Id == id).FirstOrDefault();
        
        if (deleteItem == null)
        {
            return false;
        }
        
        _toDoCollection.DeleteOne(item => item.Id == id);
        
        return true;
    }

    public List<ToDoItem> GetItemsWithFilters(Priority? priority, bool? isCompleted, DateTime? deadline)
    {
        IFindFluent<ToDoItem, ToDoItem>? result;
        
        if (priority == null && isCompleted == null && deadline == null)
        {
            // return _toDoList;
        }

        if (priority.HasValue)
        {
            result = _toDoCollection.Find(item => item.Priority == priority.Value);
        }

        if (isCompleted.HasValue)
        {
            // result = _toDoCollection.Find(item => item.IsCompleted == priority.Value);
        }

        if (deadline.HasValue)
        {
            // query = query.Where(item => item.Deadline == deadline.Value);
        }
        
        // return query.ToList();

        return new List<ToDoItem>();
    }
}