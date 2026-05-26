using Microsoft.AspNetCore.Identity;
using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users.Commands.Registration;

public class RegistrationHandler
{
    private readonly IUsersRepository _repository;

    public RegistrationHandler(IUsersRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RegistrationCommand command)
    {
        command.Username = command.Username.Trim();
        command.Password = command.Password.Trim();

        if (await _repository.GetByUsername(command.Username) is not null)
        {
            throw new InvalidOperationException("User is already registered");
        }

        if (string.IsNullOrEmpty(command.Password) || command.Password.Length < 5)
        {
            throw new InvalidOperationException("Password is required");
        }

        if (string.IsNullOrEmpty(command.Username) || command.Username.Length < 5)
        {
            throw new InvalidOperationException("Username is required");
        }

        var passwordHasher = new PasswordHasher<User>();

        var newUser = new User(command.Username, passwordHasher.HashPassword(null, command.Password));

        await _repository.RegisterUser(newUser);

        return true;
    }
}