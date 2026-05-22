using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users;

public interface IUsersRepository
{
    public Task<bool> IsLoginExist(string username);
    public Task<bool> RegisterUser(User user);
}