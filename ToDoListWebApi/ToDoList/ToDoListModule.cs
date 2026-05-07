using ToDoListWebApi.ToDoList.Commands;
using ToDoListWebApi.ToDoList.Commands.DeleteItem;
using ToDoListWebApi.ToDoList.Queries.GetAllItems;
using ToDoListWebApi.ToDoList.Queries.GetAllItemsWithFilters;

namespace ToDoListWebApi.ToDoList;

public static class ToDoListModule
{
    public static IServiceCollection AddToDoListModule(
        this IServiceCollection services)
    {
        services.AddScoped<IToDoListRepository, ToDoRepository>();
        services.AddScoped<AddItemHandler>();
        services.AddScoped<GetAllItemsHandler>();
        services.AddScoped<DeleteItemHandler>();
        services.AddScoped<GetAllItemsWithFiltersHandler>();
        
        return services;
    }
}