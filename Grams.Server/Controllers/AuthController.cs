using Microsoft.AspNetCore.Mvc;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserRegister request)
    {
        var response = await _authService
            .Register(new User
            {
                Username = request.Username,
            }, request.Password);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}
