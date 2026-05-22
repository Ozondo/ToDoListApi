using MongoDB.Driver;
using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users;

public class UsersRepository: IUsersRepository
{
    private readonly IMongoCollection<User> _userCollection;
    
    public UsersRepository(MongoDbSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(mongoSettings.UsersCollectionName);
    }

    public async Task<bool> IsLoginExist(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        var count = await _userCollection.CountDocumentsAsync(filter);

        return count > 0;
    }
    
    public async Task<bool> RegisterUser(User user)
    {
        await _userCollection.InsertOneAsync(user);
        
        return true;
    }
}