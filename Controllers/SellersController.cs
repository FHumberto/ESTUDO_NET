using System.Diagnostics;
using CNA_SalesWebMvc.Models;
using CNA_SalesWebMvc.Models.ViewModels;
using CNA_SalesWebMvc.Services;
using CNA_SalesWebMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CNA_SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // injeção de dependencia
        private readonly SellerService _sellerService;

        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync(); // encontra todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments }; // recebe a lista dos departamentos
            return View(viewModel); // envia para a view
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public async Task<IActionResult> Create(Seller seller)
        {
            // testa se o formulário é valido (previnindo bug do JS desabilitado)
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync(); // encontra todos os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // recebe a lista dos departamentos

                return View(viewModel); // retorna a view com o mesmo objeto
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index)); // Redireciona para a interface index
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index)); // Redireciona para a interface index
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            // procura os departamento para povoar a lista de departamentos
            List<Department> departments = await _departmentService.FindAllAsync();

            // preenche o formulário com os dados que já existem o OBJ buscado e os Departamentos
            SellerFormViewModel viewModel = new() { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // testa se o formulário é valido (previnindo bug do JS desabilitado)
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync(); // encontra todos os departamentos
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // recebe a lista dos departamentos

                return View(viewModel); // retorna a view com o mesmo objeto
            }
            // o ID do vendedor que tá atualizando não pode ser diferente do da requisição
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            // pode gerar excessão na camada de serviço
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                // manda a mensagem da exceção para o redirect
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        //QUANDO OCORRE O ERRO
        public IActionResult Error(string message)
        {
            // PASSA A MENSAGEM E PEGA TRACE PARA PASSAR PRA VIEW
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}