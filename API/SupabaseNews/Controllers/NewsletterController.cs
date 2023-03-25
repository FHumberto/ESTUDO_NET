using Microsoft.AspNetCore.Mvc;
using SupabaseNews.Contracts;
using SupabaseNews.Models;

namespace SupabaseNews.Controllers;

[ApiController]
[Route("[controller]")]
public class NewslettersController : ControllerBase
{
    private readonly Supabase.Client _client;

    public NewslettersController(Supabase.Client client)
    {
        _client = client;
    }

    [HttpPost("/newsletters")]
    public async Task<IActionResult> Post(CreateNewsletterRequest request)
    {
        var newsletter = new Newsletter()
        {
            Name = request.Name,
            Description = request.Description,
            ReadTime = request.ReadTime,
        };

        var response = await _client.From<Newsletter>().Insert(newsletter);

        var newsNewsletter = response.Models.First();

        return Ok(newsNewsletter.Id);
    }

    [HttpGet("/newsletters/{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var response = await _client
            .From<Newsletter>()
            .Where(n => n.Id == id)
            .Get();

        var newsletter = response.Models.FirstOrDefault();

        if (newsletter is null)
        {
            return NotFound();
        }

        // serialização para padronizar a resposta
        var newsletterResponse = new NewsletterResponse
        {
            Id = newsletter.Id,
            Name = newsletter.Name,
            Description = newsletter.Description,
            ReadTime = newsletter.ReadTime,
            CreatedAt = newsletter.CreatedAt
        };

        return Ok(newsletterResponse);
    }

    [HttpDelete("newsletter/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _client
            .From<Newsletter>()
            .Where(n => n.Id == id)
            .Delete();

        return NoContent();
    }
}
