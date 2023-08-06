using AutoMapper;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

[Route("api/ControllerApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly Iloging _logger;
    private readonly IVillaRepository _repoVilla;
    private readonly IMapper _mapper;

    public VillaApiController(Iloging logger, IVillaRepository repoVilla, IMapper mapper)
    {
        _logger = logger;
        _repoVilla = repoVilla;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
    {
        _logger.Log("Getting all villas", "");
        IEnumerable<Villa> villaList = await _repoVilla.GetAllAsync();
        return Ok(_mapper.Map<List<VillaDto>>(villaList));
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VillaDto>> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.Log("Get villa Error with Id" + id, "error");
            return BadRequest();
        }

        Villa villa = await _repoVilla.GetAsync(x => x.Id == id);

        if (villa is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VillaDto>(villa));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto createDto)
    {
        if (await _repoVilla.GetAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");
            return BadRequest(ModelState);
        }

        if (createDto == null)
        {
            return BadRequest();
        }

        Villa model = _mapper.Map<Villa>(createDto);

        await _repoVilla.CreateAsync(model);

        return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public async Task<ActionResult<VillaDto>> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        Villa villa = await _repoVilla.GetAsync(u => u.Id == id);

        if (villa == null)
        {
            return NotFound();
        }

        await _repoVilla.RemoveAsync(villa);

        return Ok("Deleted");
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
    {
        if (updateDto == null || id != updateDto.Id)
        {
            return BadRequest();
        }

        Villa model = _mapper.Map<Villa>(updateDto);

        await _repoVilla.UpdateAsync(model);

        return Ok("Updated");
    }

    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }

        Villa villa = await _repoVilla.GetAsync(u => u.Id == id, tracked: false);

        VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

        if (villa == null)
        {
            return BadRequest();
        }

        patchDto.ApplyTo(villaDto, ModelState);

        Villa model = _mapper.Map<Villa>(villaDto);

        await _repoVilla.UpdateAsync(model);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok("Updated");
    }
}
