using ToDoListWebApi.Users.Commands.Login;
using ToDoListWebApi.Users.Commands.Registration;
using ToDoListWebApi.Users.IdAcess;

namespace ToDoListWebApi.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<RegistrationHandler>();
        services.AddScoped<LoginHandler>();

        return services;
    }
}