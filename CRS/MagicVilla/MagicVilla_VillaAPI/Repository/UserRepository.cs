using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private string _secretKey;

    public UserRepository(ApplicationDbContext db, IConfiguration configuration)
    {
        _db = db;
        _secretKey = configuration.GetValue<string>("ApiSettrings:Secret");
    }

    public bool IsUniqueUser(string username)
    {
        // checa se existe um usuário na database
        var user = _db.LocalUsers.FirstOrDefault(x => x.UserName == username);

        if (user == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        // pesquisa no banco se o usuário existe
        var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower()
    && u.Password == loginRequestDto.Password);

        if (user == null)
        {
            return new LoginResponseDto()
            {
                Token = "",
                User = null
            };
        }

        //se usuario encontrado gera jwt token
        var tokenHandler = new JwtSecurityTokenHandler();

        // converte a chave em byte
        var key = Encoding.ASCII.GetBytes(_secretKey);

        // token descriotor vai conter tudo do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7), // quando o token expira
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor); // faz a criação do token

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            Token = tokenHandler.WriteToken(token),
            User = user
        };

        return loginResponseDto;
    }

    public async Task<LocalUser> Register(RegistrationRequestDto registrationRequestDto)
    {
        // cria e serializa o novo usuário
        LocalUser user = new()
        {
            UserName = registrationRequestDto.UserName,
            Password = registrationRequestDto.Password,
            Name = registrationRequestDto.Name,
            Role = registrationRequestDto.Role,
        };

        _db.LocalUsers.Add(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }
}
