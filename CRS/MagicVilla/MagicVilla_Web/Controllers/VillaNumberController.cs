using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService, IMapper mapper)
        {
            _villaNumberService = villaNumberService;
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new();

            var response = await _villaNumberService.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CreateVillaNumber()
        {
            // get all villa number e dela pegar todos os nomes de villa e mandar para a view.
            VillaNumberCreateVM villaNumberVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                // alimenta a lista de nomes no view model.
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {

                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if(response.ErrorMessages.Count > 0)
                    {
                        // adiciona os erros encontrados a chave.
                        ModelState.AddModelError("ErrorMEssages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            // popula novamente o model
            var resp = await _villaService.GetAllAsync<APIResponse>();

            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(model);
        }

        //// antes de dá update mostrar na tela o que o usuário vai atualizar
        //public async Task<IActionResult> UpdateVillaNumber(int villaId)
        //{
        //    var response = await _villaService.GetAsync<APIResponse>(villaId);

        //    if (response != null && response.IsSuccess)
        //    {
        //        VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
        //        return View(_mapper.Map<VillaUpdateDto>(model));
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVillaNumber(VillaUpdateDto model)
        //{
        //    // validação presentes no data anotation
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _villaService.UpdateAsync<APIResponse>(model);

        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(IndexVilla));
        //        }
        //    }

        //    return View(model); // retorna o modelo com os erro
        //}

        //public async Task<IActionResult> DeleteVillaNumber(int villaId)
        //{
        //    var response = await _villaService.GetAsync<APIResponse>(villaId);

        //    if (response != null && response.IsSuccess)
        //    {
        //        VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
        //        return View(_mapper.Map<VillaDto>(model));
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteVillaNumber(VillaDto model)
        //{
        //    var response = await _villaService.DeleteAsync<APIResponse>(model.Id);

        //    if (response != null && response.IsSuccess)
        //    {
        //        return RedirectToAction(nameof(IndexVilla));
        //    }

        //    return View(model);
        //}
    }
}
