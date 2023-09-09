using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager; // injeção de dependencia de usuário identitys
    private readonly RoleManager<IdentityRole> _roleManager;
    private string _secretKey;
    private readonly IMapper _mapper; // automapper

    public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager,  IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
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
        //    var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower()
        //&& u.Password == loginRequestDto.Password);

        //! utilizando o identity
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || isValid == false)
        {
            return new LoginResponseDto()
            {
                Token = "",
                User = null
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        //se usuario encontrado gera jwt token
        var tokenHandler = new JwtSecurityTokenHandler();

        // converte a chave em byte
        var key = Encoding.ASCII.GetBytes(_secretKey);

        // token descriotor vai conter tudo do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault()) // se tiver mais de uma (foreach aqui)
            }),
            Expires = DateTime.UtcNow.AddDays(7), // quando o token expira
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor); // faz a criação do token

        LoginResponseDto loginResponseDto = new LoginResponseDto()
        {
            Token = tokenHandler.WriteToken(token),
            User = _mapper.Map<UserDto>(user),
            //Role = roles.FirstOrDefault(),
        };

        return loginResponseDto;
    }

    public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
    {
        // cria e serializa o novo usuário
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.UserName,
            Email = registrationRequestDto.UserName,
            NormalizedEmail = registrationRequestDto.UserName.ToUpper(),
            Name = registrationRequestDto.Name,
        };

        try
        {
            // automaticamente faz o hash no password e cria o usuário
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                // se as roles não existirem no database, cria //! outra forma seria adicionar diretamente nas migration
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("customer"));
                }

                await _userManager.AddToRoleAsync(user, "admin");

                var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDto.UserName);

                return _mapper.Map<UserDto>(userToReturn);
            }
        }
        catch (Exception ex)
        {
            //! lembrar de adicionar uma validação para retornar se o password não é seguro o suficiente
        }

        //_db.LocalUsers.Add(user);
        //await _db.SaveChangesAsync();
        //user.Password = "";
        return new UserDto();
    }
}
