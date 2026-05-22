using ToDoListWebApi;
using ToDoListWebApi.ToDoList;
using ToDoListWebApi.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// MongoDB Configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton(serviceProvider =>
{
    var options = serviceProvider.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<MongoDbSettings>>();
    return options.Value;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Модули приложения
builder.Services.AddToDoListModule();
builder.Services.AddUsersModule();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
