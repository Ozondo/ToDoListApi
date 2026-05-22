using ToDoListWebApi.Users.Commands.Registration;

namespace ToDoListWebApi.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<RegistrationHandler>();
        
        return services;
    }
}