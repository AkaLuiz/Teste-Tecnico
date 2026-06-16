using Microsoft.AspNetCore.Mvc;
namespace AuthService.Controller;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var response = await _authService.Login(request);
        return Ok(response);
    }

    [HttpPost("usuarios")]
    public async Task<ActionResult<Usuario>> CriarUsuario(RegistrarUsuarioRequest request)
    {
        var usuario = await _authService.CriarUsuario(request);
        return Created($"/auth/usuarios/{usuario.Id}", usuario);
    }
}