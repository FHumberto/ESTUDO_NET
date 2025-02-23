using ConfigurationApi.Model;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigsController : ControllerBase
{
    //? carrega os dados do configuration (hotreload)
    private readonly IConfiguration _configuration;

    //? carrega os dados na inicialização (imutável).
    private readonly IOptions<InfraSettings> _settings;

    //? carrega os dados antes de cada scopo (início da request e fim da request), se no intervalo mudar, atualizará
    private readonly IOptionsSnapshot<InfraSettings> _settingsSnapshot;

    //? vai ler o valor mais atualizado, não importa o state (hotreload)
    private readonly IOptionsMonitor<InfraSettings> _infraMonitor;

    public ConfigsController
        (
            IConfiguration configuration,
            IOptions<InfraSettings> settings,
            IOptionsSnapshot<InfraSettings> optionsSnapshot,
            IOptionsMonitor<InfraSettings> infraMonitor
        )
    {
        _configuration = configuration;
        _settings = settings;
        _settingsSnapshot = optionsSnapshot;
        _infraMonitor = infraMonitor;
    }

    [HttpGet("/prop")]
    public IActionResult Index()
    {
        return Ok(_configuration["Name"]);
    }

    //? scopo entrada de request e saida de request
    [HttpGet("/record")]
    public async Task<IActionResult> Record()
    {
        var settings = _settings.Value;
        var settingsSnapshot = _settingsSnapshot.Value;

        await Task.Delay(10000);

        var configSnapshot = _settingsSnapshot.Get("InfraSettings");

        var resposta = new
        {
            Settings = settings,
            SettingsSnapshot = settingsSnapshot
        };

        return Ok(resposta);
    }
}
