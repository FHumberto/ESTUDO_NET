using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersCatalog.DataAccess.Data;
using UsersCatalog.Models;
using UsersCatalog.Models.DTOs;
using UsersCatalog.Models.Models;

namespace UsersCatalog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _db;

    public UserController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(_db.Users.ToList());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> GetById(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var user = _db.Users.Find(id);

        if (user is null)
        {
            return NotFound();
        }

        UserDto userDto = new UserDto();

        userDto.FirstName = user.FirstName;
        userDto.LastName = user.LastName;
        userDto.Email = user.Email;

        return Ok(userDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<UserDto> CreateUser([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var newUser = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
        };

        _db.Users.Add(newUser);
        _db.SaveChanges();

        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> UpdateUser(int id, [FromBody] UserDto userDto)
    {
        if (userDto is null)
        {
            return BadRequest();
        }

        var user = _db.Users.Find(id);

        if (user is null)
        {
            return NotFound();
        }

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Email = userDto.Email;

        _db.Update(user);
        _db.SaveChanges();

        return Ok(userDto);
    }

    [HttpDelete("{id:int}", Name = "Delete User")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> DeleteUser (int id)
    {
        if(id == 0)
        {
            return BadRequest();
        }

        var user = _db.Users.Find(id);

        if(user is null)
        {
            return NotFound();
        }

        _db.Remove(user);
        _db.SaveChanges();

        return Ok("Usuário removido.");
    }
}
