using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Utility;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _villaUrl;

        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _villaUrl = configuration.GetValue<string>("ServiceUrl:VillaAPI");
        }

        Task<T> IVillaNumberService.CreateAsync<T>(VillaNumberCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{_villaUrl}/api/villaNumberAPI"
            });
        }

        Task<T> IVillaNumberService.DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{_villaUrl}/api/villaNumberAPI/{id}"
            });
        }

        Task<T> IVillaNumberService.GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_villaUrl}/api/villaNumberAPI/"
            });
        }

        Task<T> IVillaNumberService.GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_villaUrl}/api/villaNumberAPI/{id}"
            });
        }

        Task<T> IVillaNumberService.UpdateAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto, // o que vai pelo corpo
                Url = $"{_villaUrl}/api/villaNumberAPI/{dto.VillaNo}"
            });
        }
    }
}
