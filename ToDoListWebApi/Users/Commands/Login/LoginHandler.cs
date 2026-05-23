using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListWebApi.Users.Entities;

namespace ToDoListWebApi.Users.Commands.Login;

public class LoginHandler
{
    private readonly IUsersRepository _repository;
    private readonly JwtSettings _jwtSettings;

    public LoginHandler(IUsersRepository repository, IOptions<JwtSettings> jwtSettings)
    {
        _repository = repository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResponce> Handle(LoginCommand command)
    {
        command.Username = command.Username.Trim();
        command.Password = command.Password.Trim();

        var user = await _repository.GetByUsername(command.Username);
        if (user is null)
        {
            throw new InvalidOperationException("Invalid username or password");
        }

        var passwordHasher = new PasswordHasher<User>();
        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, command.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException("Invalid username or password");
        }

        var token = GenerateToken(user);

        return new LoginResponce(token);
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
