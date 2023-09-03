using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers;

[Route("api/VillaNumberApi")]
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumberList = await _repoVillaNumber.GetAllAsync(includeProperties: "Villa");
            _response.Result = _mapper.Map<List<VillaNumberDto>>(villaNumberList);
            _response.StatusCode = HttpStatusCode.OK;
            _logger.Log("Getting all villas numbers", "");
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpGet("{id:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse>> GetVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _logger.Log("Get villa number Error with VillaNo" + id, "error");
                return BadRequest(_response);
            }

            VillaNumber villaNumber = await _repoVillaNumber.GetAsync(u => u.VillaNo == id);

            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
    {
        try
        {

            if (await _repoVillaNumber.GetAsync(u => u.SpecialDetails.ToLower() == createDto.SpecialDetails.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "VillaNummber SpecialDetails already Exists!");
                return BadRequest(ModelState);
            }

            if (await _repoVilla.GetAsync(u => u.Id == createDto.VillaID) == null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDto);

            await _repoVillaNumber.CreateAsync(villaNumber);
            _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    public async Task<ActionResult<ApiResponse>> DeleteVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest();
            }

            VillaNumber villaNumber = await _repoVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null)
            {
                return NotFound();
            }

            await _repoVillaNumber.RemoveAsync(villaNumber);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto updateDto)
    {
        try
        {
            if (updateDto == null || id != updateDto.VillaNo)
            {
                return BadRequest();
            }

            if (await _repoVilla.GetAsync(u => u.Id == updateDto.VillaID) == null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

            await _repoVillaNumber.UpdateAsync(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}
