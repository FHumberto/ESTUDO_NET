using DapperApi.Data;
using DapperApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var produtos = await _produtoRepository.GetAll();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _produtoRepository.GetById(id);
        if (produto == null)
            return NotFound();
        return Ok(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Produto produto)
    {
        var result = await _produtoRepository.Add(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Produto produto)
    {
        if (id != produto.Id)
            return BadRequest();

        var result = await _produtoRepository.Update(produto);
        if (result == 0)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _produtoRepository.Delete(id);
        if (result == 0)
            return NotFound();

        return NoContent();
    }
}
