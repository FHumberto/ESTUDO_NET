using ConsumirAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumirAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> GetByName(string name)
    {
        var apiUrl = $"https://kitsu.io/api/edge/manga?filter[text]={name}";
        var response = await _httpClient.GetAsync(apiUrl);

        if (response == null)
        {
            return NotFound();
        }
        else
        {
            var jsonString = await response.Content.ReadAsStringAsync(); // converte para json
            Manga jsonObject = JsonConvert.DeserializeObject<Manga>(jsonString);
            return Ok(jsonObject);
        }
    }
}
