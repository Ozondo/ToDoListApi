namespace ToDoListWebApi.Users.IdAcess;

public interface ICurrentUserContext
{
    string GetRequiredUserId();
}
