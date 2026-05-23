using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;

namespace ToDoListWebApi.ToDoList;

public class ToDoRepository: IToDoListRepository
{
    private const string _collectionName = "ToDoItems";

    private readonly IMongoCollection<ToDoItem> _toDoCollection;
    
    public ToDoRepository(MongoDbSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

        _toDoCollection = mongoDatabase.GetCollection<ToDoItem>(_collectionName);
    }
    
    public async Task<List<ToDoItem>> GetAll()
    {
        return await _toDoCollection.Find(_ => true).ToListAsync();
    }

    public bool AddItem(ToDoItem item)
    {
        _toDoCollection.InsertOne(item);

        return true;
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

    public List<ToDoItem> GetItemsWithFilters(FilterDefinition<ToDoItem> filter)
    {
        return _toDoCollection.Find(filter).ToList();
    }
}