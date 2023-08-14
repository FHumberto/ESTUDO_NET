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
        _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
    }

    Task<T> IVillaService.CreateAsync<T>(VillaCreateDto dto)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = $"{_villaUrl}/api/villaAPI"
        });
    }

    Task<T> IVillaService.DeleteAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.DELETE,
            Url = $"{_villaUrl}/api/villaAPI/{id}"
        });
    }

    Task<T> IVillaService.GetAllAsync<T>()
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{_villaUrl}/api/villaAPI/"
        });
    }

    Task<T> IVillaService.GetAsync<T>(int id)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = $"{_villaUrl}/api/villaAPI/{id}"
        });
    }

    Task<T> IVillaService.UpdateAsync<T>(VillaUpdateDto dto)
    {
        return SendAsync<T>(new APIRequest()
        {
            ApiType = SD.ApiType.PUT,
            Data = dto, // o que vai pelo corpo
            Url = $"{_villaUrl}/api/villaAPI/{dto.Id}"
        });
    }
}
