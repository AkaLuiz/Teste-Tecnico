using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace AuthService.Controllers;

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
        try
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }catch(UnauthorizedAccessException e)
        {
            return BadRequest(e.Message);
        }

    }

    [HttpPost("usuarios")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Usuario>> CriarUsuario(RegistrarUsuarioRequest request)
    {
        var usuario = await _authService.CriarUsuario(request);
        return Created($"/auth/usuarios/{usuario.Id}", usuario);
    }

    [HttpGet("usuarios")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<Usuario>>> ListarUsuarios()
    {
        var usuarios = await _authService.ListarUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("usuarios/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Usuario>> BuscarPorId(Guid id)
    {
        var usuario = await _authService.BuscarPorId(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<Usuario>> ObterInformacoesDoUsuarioLogado()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }
        var usuario = await _authService.BuscarPorId(Guid.Parse(userId));
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }
}