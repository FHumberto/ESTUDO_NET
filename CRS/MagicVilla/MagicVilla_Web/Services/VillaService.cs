using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services;

public class VillaService : BaseService, IVillaService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _villaUrl;

    public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        _villaUrl = configuration.GetValue<string>("ServiceUrl:VillaAPI");
    }

    Task<T> IVillaService.CreateAsync<T>(VillaCreateDto dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = $"{_villaUrl}/api/villaAPI",
            Token = token
        });
    }

    Task<T> IVillaService.DeleteAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.DELETE,
            Url = $"{_villaUrl}/api/villaAPI/{id}",
            Token = token
        });
    }

    Task<T> IVillaService.GetAllAsync<T>(string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{_villaUrl}/api/villaAPI/",
            Token = token
        });
    }

    Task<T> IVillaService.GetAsync<T>(int id, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{_villaUrl}/api/villaAPI/{id}",
            Token = token
        });
    }

    Task<T> IVillaService.UpdateAsync<T>(VillaUpdateDto dto, string token)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.PUT,
            Data = dto, // o que vai pelo corpo
            Url = $"{_villaUrl}/api/villaAPI/{dto.Id}",
            Token = token
        });
    }
}
