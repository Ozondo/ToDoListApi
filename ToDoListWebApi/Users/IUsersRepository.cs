using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users;

public interface IUsersRepository
{
    public Task<bool> RegisterUser(User user);

    public Task<User?> GetByUsername(string username);
}