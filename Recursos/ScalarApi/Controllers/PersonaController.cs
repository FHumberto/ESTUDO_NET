using Microsoft.AspNetCore.Mvc;
using ScalarApi.Services;

namespace ScalarApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonaController : Controller
{
    private readonly PersonaRepository _repository;

    public PersonaController(PersonaRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _repository.GetPersonasAsync();

        if (!response.Any())
        {
            return NotFound();
        }

        return Ok(response);
    }
}
