using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Users.Commands.Login;
using ToDoListWebApi.Users.Commands.Registration;

namespace ToDoListWebApi.Users;

[ApiController]
[Route("[controller]")]
public class UsersController(
    RegistrationHandler registrationHandler,
    LoginHandler loginHandler) : ControllerBase
{
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegistrationCommand user)
    {
        try
        {
            await registrationHandler.Handle(user);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        try
        {
            var result = await loginHandler.Handle(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}