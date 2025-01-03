using CodeCommandos.Domain;
using CodeCommandos.Domain.Dtos;
using CodeCommandos.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCommandos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ExpenseTrackingContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(ExpenseTrackingContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            return BadRequest("Email already in use.");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password.");
        }

        var token = JwtHelper.GenerateJwtToken(user.Email, _configuration);
        return Ok(new { Token = token });
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        var googleId = request.GoogleId; // Verify with Google API if required.
        var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);

        if (user == null)
        {
            user = new User
            {
                GoogleId = googleId,
                Email = request.Email,
                Username = request.Name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        var token = JwtHelper.GenerateJwtToken(user.Email, _configuration);
        return Ok(new { Token = token });
    }
}
