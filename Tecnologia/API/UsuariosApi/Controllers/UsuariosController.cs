using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Data;
using UsuariosApi.Domain.InputModels;
using UsuariosApi.Domain.Models;

namespace UsuariosApi.Controllers;
[Route("api/v1/usuario")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly UsuariosDbContext _dados;

    public UsuarioController(UsuariosDbContext dados)
    {
        _dados = dados;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Usuario>? usuarios = await _dados.Usuarios.ToListAsync();

        return usuarios is null ? NoContent() : Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int? id)
    {
        Usuario? usuario = await _dados.Usuarios.FindAsync(id);

        return usuario is null ? NotFound() : !usuario.IsValid ? BadRequest(usuario.Notifications) : Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioInputModel usuarioRequest)
    {
        Usuario usuario = new(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Email);

        if (!usuario.IsValid)
        {
            return BadRequest(usuario.Notifications);
        }

        _ = await _dados.Usuarios.AddAsync(usuario);
        _ = await _dados.SaveChangesAsync();

        return Created($"/usuario/{usuario.Id}", usuario.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromRoute] int id, [FromRoute] UsuarioInputModel usuarioRequest)
    {
        Usuario? usuario = await _dados.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return NotFound();
        }

        usuario.Editar(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Email);

        if (!usuario.IsValid)
        {
            return BadRequest(usuario.Notifications);
        }

        _ = await _dados.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Usuario? usuario = await _dados.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
        {
            return NotFound();
        }

        _ = _dados.Usuarios.Remove(usuario);
        _ = await _dados.SaveChangesAsync();

        return Ok();
    }
}
