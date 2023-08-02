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

    public VillaApiController(Iloging logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        _logger.Log("Getting all villas", "");
        return Ok(_db.Villas.ToList());
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id == 0)
        {
            _logger.Log("Get villa Error with Id" + id, "error");
            return BadRequest();
        }

        var villa = _db.Villas.FirstOrDefault(x => x.Id == id);

        if (villa is null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
    {
        if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");
            return BadRequest(ModelState);
        }

        if (villaDto == null)
        {
            return BadRequest();
        }
        if (villaDto.Id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // Serialização do Dto para Villa
        Villa model = new()
        {
            Amenity = villaDto.Amenity,
            Details = villaDto.Details,
            Id = villaDto.Id,
            ImageUrl = villaDto.ImageUrl,
            Name = villaDto.Name,
            Occupancy = villaDto.Occupancy,
            Rate = villaDto.Rate,
            Sqft = villaDto.Sqft,
        };

        _db.Villas.AddAsync(model);
        _db.SaveChanges();

        return CreatedAtRoute("GetVilla", model.Id, villaDto);
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public ActionResult<VillaDto> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = _db.Villas.Find(id);

        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        _db.SaveChanges();

        return Ok("Deleted");
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
    {
        if (villaDto == null || id != villaDto.Id)
        {
            return BadRequest();
        }

        var villa = _db.Villas.Find(id);

        _db.Villas.Update(villa);
        _db.SaveChanges();

        return Ok("Updated");
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
    {
        if (patchDto == null || id == 0)
        {
            return BadRequest();
        }

        var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

        // serialização p/ dto
        VillaDto villaDto = new()
        {
            Amenity = villa.Amenity,
            Details = villa.Details,
            Id = villa.Id,
            ImageUrl = villa.ImageUrl,
            Name = villa.Name,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
        };

        if (villa is null)
        {
            return NotFound();
        }

        patchDto.ApplyTo(villaDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Villa model = new()
        {
            Amenity = villaDto.Amenity,
            Details = villaDto.Details,
            Id = villa.Id,
            ImageUrl = villa.ImageUrl,
            Name = villa.Name,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft,
        };

        _db.Villas.Update(model);
        _db.SaveChanges();

        return Ok("Updated");
    }
}
