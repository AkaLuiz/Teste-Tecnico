using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RegistrosService.Enums;
namespace RegistrosService.Controllers;

[ApiController]
[Route("registros")]
public class RegistrosController : ControllerBase
{
    private readonly IRegistroService _registroService;

    public RegistrosController(IRegistroService registroService)
    {
        _registroService = registroService;
    }

    [Authorize(Roles = "Admin,Registrador")]
    [HttpPost]
    public async Task<ActionResult<Registro>> CriarRegistro(CadastrarRegistroRequest request)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var registro = await _registroService.CriarRegistro(request, Guid.Parse(userId));

            return Created($"registros/{registro.Id}", registro);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Registro>>> ListarRegistros([FromQuery] RegistroFiltroRequest filtro)
    {
        var registros = await _registroService.ListarRegistros(filtro);
        return Ok(registros);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Registro>> BuscarPorId(Guid id)
    {
        var registro = await _registroService.BuscarPorId(id);
        if (registro == null)
            return NotFound();

        return Ok(registro);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Registrador")]
    public async Task<ActionResult<Registro>> AtualizarRegistro(Guid id, AtualizarRegistroRequest request)
    {
        try
        {
            var registro = await _registroService.AtualizarRegistro(id, request);
            if (registro == null)
                return NotFound();

            return Ok(registro);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin,Registrador")]
    public async Task<ActionResult<Registro>> AlterarStatusPedido(Guid id, AlterarStatusRequest novoStatus)
    {
        try
        {
            var registro = await _registroService.AlterarStatus(id, novoStatus);
            if (registro == null)
                return NotFound();

            return Ok(registro);
        }
        catch (InvalidOperationException e)
        {
            return UnprocessableEntity(e.Message);
        }

    }

    //delete
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Registro>> DeletarPorId(Guid id)
    {
        var registro = await _registroService.DeletarPorId(id);

        if (registro == null)
            return NotFound();

        return NoContent();

    }
}