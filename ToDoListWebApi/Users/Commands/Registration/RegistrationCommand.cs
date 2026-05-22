using System.ComponentModel.DataAnnotations;

namespace ToDoListWebApi.Users.Commands.Registration;

public class RegistrationCommand
{
    [Required]
    [MinLength(5)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [MinLength(5)]
    public string Password { get; set; } = string.Empty;
}