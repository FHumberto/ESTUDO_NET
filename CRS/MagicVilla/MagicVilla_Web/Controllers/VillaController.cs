using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers;

public class VillaController : Controller
{
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;

    public VillaController(IVillaService villaService, IMapper mapper)
    {
        _villaService = villaService;
        _mapper = mapper;
    }

    public async Task<IActionResult> IndexVilla()
    {
        List<VillaDto> list = new();

        var response = await _villaService.GetAllAsync<APIResponse>();

        if(response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
        }

        return View(list);
    }

    public IActionResult CreateVilla()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVilla(VillaCreateDto model)
    {
        // validação presentes no data anotation
        if(ModelState.IsValid)
        {
            var response = await _villaService.CreateAsync<APIResponse>(model);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
        }

        return View(model); // retorna o modelo com os erro
    }

    // antes de dá update mostrar na tela o que o usuário vai atualizar
    public async Task<IActionResult> UpdateVilla(int villaId)
    {
        var response = await _villaService.GetAsync<APIResponse>(villaId);

        if (response != null && response.IsSuccess)
        {
            VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaUpdateDto>(model));
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateVilla(VillaUpdateDto model)
    {
        // validação presentes no data anotation
        if (ModelState.IsValid)
        {
            var response = await _villaService.UpdateAsync<APIResponse>(model);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
        }

        return View(model); // retorna o modelo com os erro
    }

    public async Task<IActionResult> DeleteVilla(int villaId)
    {
        var response = await _villaService.GetAsync<APIResponse>(villaId);

        if (response != null && response.IsSuccess)
        {
            VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
            return View(_mapper.Map<VillaDto>(model));
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteVilla(VillaDto model)
    {
        var response = await _villaService.DeleteAsync<APIResponse>(model.Id);

        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(IndexVilla));
        }

        return View(model);
    }
}
