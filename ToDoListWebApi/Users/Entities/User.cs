using MongoDB.Bson.Serialization.Attributes;

namespace ToDoListWebApi.Users.Entities;

public class User
{
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    [BsonId]
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string Username { get; set; }
    public string Password { get; set; }
}