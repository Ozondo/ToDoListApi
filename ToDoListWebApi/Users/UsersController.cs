using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Users.Commands.Registration;
using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users;

[ApiController]
[Route("[controller]")]
public class UsersController: ControllerBase
{
    private readonly RegistrationHandler _registrationHandler;

    public UsersController(RegistrationHandler registrationHandler)
    {
        _registrationHandler = registrationHandler;
    }
    
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegistrationCommand user)   
    {
        try
        {
            await _registrationHandler.Handle(user);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}