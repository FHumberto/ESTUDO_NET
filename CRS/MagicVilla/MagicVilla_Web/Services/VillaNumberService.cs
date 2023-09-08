using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

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

        Task<T> IVillaNumberService.CreateAsync<T>(VillaNumberCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $"{_villaUrl}/api/v1/villaNumberAPI",
                Token = token
            });
        }

        Task<T> IVillaNumberService.DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{_villaUrl}/api/v1/villaNumberAPI/{id}",
                Token = token
            });
        }

        Task<T> IVillaNumberService.GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_villaUrl}/api/v1/villaNumberAPI/",
                Token = token
            });
        }

        Task<T> IVillaNumberService.GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{_villaUrl}/api/v1/villaNumberAPI/{id}",
                Token = token
            });
        }

        Task<T> IVillaNumberService.UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto, // o que vai pelo corpo
                Url = $"{_villaUrl}/api/v1/villaNumberAPI/{dto.VillaNo}",
                Token = token
            });
        }
    }
}
