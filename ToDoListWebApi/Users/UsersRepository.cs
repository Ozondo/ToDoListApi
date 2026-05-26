using MongoDB.Driver;
using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users;

public class UsersRepository : IUsersRepository
{
    private const string _collectionName = "Users";
    private readonly IMongoCollection<User> _userCollection;

    public UsersRepository(MongoDbSettings mongoSettings)
    {
        var mongoClient = new MongoClient(mongoSettings.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoSettings.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(_collectionName);
    }

    public async Task<bool> RegisterUser(User user)
    {
        await _userCollection.InsertOneAsync(user);

        return true;
    }

    public async Task<User?> GetByUsername(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }
}