using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2;

[Route("api/v{version:apiVersion}/VillaNumberApi")]
[ApiVersion("2.0")]
[ApiController]
public class VillaNumberApiController : ControllerBase
{
    protected ApiResponse _response;
    private readonly Iloging _logger;
    private readonly IVillaNumberRepository _repoVillaNumber;
    private readonly IVillaRepository _repoVilla;
    private readonly IMapper _mapper;

    public VillaNumberApiController(Iloging logger, IVillaRepository repoVilla, IVillaNumberRepository repoVillaNumber, IMapper mapper)
    {
        _logger = logger;
        _repoVillaNumber = repoVillaNumber;
        _mapper = mapper;
        _repoVilla = repoVilla;
        this._response = new();
    }

    //[MapToApiVersion("2.0")]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
}
