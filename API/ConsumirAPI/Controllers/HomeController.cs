using ConsumirAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumirAPI.Controllers;

/*
 * Buscar mangá por nome (recebe string) retorna o Id
 */

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

    /// <summary>
    /// Busca a comic por nome
    /// </summary>
    /// <param name="name">Student Model</param>
    /// <remarks>Busca a comic por nome</remarks>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost, Route("/buscarpornome")]
    public async Task<IActionResult> SearchMangaByName(string name)
    {
        string apiUrl = $"https://kitsu.io/api/edge/manga?filter[text]={name}";
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        if (response == null)
        {
            return NotFound();
        }
        else
        {
            string jsonString = await response.Content.ReadAsStringAsync(); // converte para json
            Manga? jsonObject = JsonConvert.DeserializeObject<Manga>(jsonString);
            return Ok(jsonObject);
        }
    }
}
