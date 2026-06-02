using MongoDB.Driver;
using ToDoListWebApi.Domain.Entities;
using ToDoListWebApi.Users.IdAcess;

namespace ToDoListWebApi.ToDoList;

public class ToDoRepository: IToDoListRepository
{
    private const string _collectionName = "ToDoItems";

    private readonly IMongoCollection<ToDoItem> _toDoCollection;
    private readonly ICurrentUserContext _currentUser;
    
    public ToDoRepository(MongoDbSettings mongoSettings, ICurrentUserContext currentUser)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

        _toDoCollection = mongoDatabase.GetCollection<ToDoItem>(_collectionName);
        _currentUser = currentUser;
    }
    
    public async Task<List<ToDoItem>> GetAll()
    {
        return await _toDoCollection.Find(e => e.UserId == _currentUser.GetRequiredUserId()).ToListAsync();
    }

    public async Task<bool> AddItem(ToDoItem item)
    {
        await _toDoCollection.InsertOneAsync(item);

        return true;
    }

    public async Task<bool> DeleteItem(string id)
    {
        var deleteItem = _toDoCollection.Find(item => item.Id == id && item.UserId == _currentUser.GetRequiredUserId()).FirstOrDefault();
        
        if (deleteItem == null)
        {
            throw new InvalidOperationException("Item with this id does not exist");
        }
        
        await _toDoCollection.DeleteOneAsync(item => item.Id == id);
        
        return true;
    }

    public async Task<List<ToDoItem>> GetItemsWithFilters(FilterDefinition<ToDoItem> filter)
    {
        return await _toDoCollection.Find(filter).ToListAsync();
    }
}