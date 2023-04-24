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
        var usuarios = await _dados.Usuarios.ToListAsync();

        if (usuarios is null)
        {
            return NoContent();
        }

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int? id)
    {
        var usuario = await _dados.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return NotFound();
        }

        if (!usuario.IsValid)
        {
            return BadRequest(usuario.Notifications);
        }

        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioInputModel usuarioRequest)
    {
        var usuario = new Usuario(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Email);

        if (!usuario.IsValid)
        {
            return BadRequest(usuario.Notifications);
        }

        await _dados.Usuarios.AddAsync(usuario);
        await _dados.SaveChangesAsync();

        return Created($"/usuario/{usuario.Id}", usuario.Id);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromRoute] int id, [FromRoute] UsuarioInputModel usuarioRequest)
    {
        var usuario = await _dados.Usuarios.FindAsync(id);

        if (usuario is null)
        {
            return NotFound();
        }

        usuario.Editar(usuarioRequest.Nome, usuarioRequest.Sobrenome, usuarioRequest.Email);

        if (!usuario.IsValid)
        {
            return BadRequest(usuario.Notifications);
        }

        await _dados.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var usuario = await _dados.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
        {
            return NotFound();
        }

        _dados.Usuarios.Remove(usuario);
        await _dados.SaveChangesAsync();

        return Ok();
    }
}
