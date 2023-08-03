using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers;

[Route("api/ControllerApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly Iloging _logger;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public VillaApiController(Iloging logger, ApplicationDbContext db, IMapper mapper)
    {
        _logger = logger;
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
    {
        _logger.Log("Getting all villas", "");
        IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
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

        Villa villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);

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
        if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");
            return BadRequest(ModelState);
        }

        if (createDto == null)
        {
            return BadRequest();
        }

        Villa model = _mapper.Map<Villa>(createDto);

        await _db.Villas.AddAsync(model);
        await _db.SaveChangesAsync();

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

        Villa villa = await _db.Villas.FindAsync(id);

        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();

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

        _db.Villas.Update(model);
        await _db.SaveChangesAsync();

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

        Villa villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

        if (villa == null)
        {
            return BadRequest();
        }

        patchDto.ApplyTo(villaDto, ModelState);

        Villa model = _mapper.Map<Villa>(villaDto);

        _db.Villas.Update(model);
        await _db.SaveChangesAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok("Updated");
    }
}
