using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers;

//[Route("api/[controller]")]
[Route("api/ControllerApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    private readonly Iloging _logger;

    public VillaApiController(Iloging logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDto>> GetVillas()
    {
        _logger.Log("Getting all villas", "");
        return Ok(VillaStore.VillaList);
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

        VillaDto villa = VillaStore.VillaList.Find(x => x.Id == id);

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
        //Se não estiver utilizando o [ApiController]
        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(ModelState);
        //}
        if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
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

        villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.VillaList.Add(villaDto);
        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
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

        VillaDto villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

        if (villa == null)
        {
            return NotFound();
        }

        VillaStore.VillaList.Remove(villa);

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

        VillaDto villa = VillaStore.VillaList.Find(u => u.Id == id);

        // serializando
        villa.Name = villaDto.Name;
        villa.Occupancy = villaDto.Occupancy;
        villa.Sqft = villaDto.Sqft;

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

        VillaDto villa = VillaStore.VillaList.Find(u => u.Id == id);

        if (villa is null)
        {
            return NotFound();
        }

        patchDto.ApplyTo(villa, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok();
    }
}
