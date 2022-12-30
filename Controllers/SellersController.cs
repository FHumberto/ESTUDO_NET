using CNA_SalesWebMvc.Models;
using CNA_SalesWebMvc.Models.ViewModels;
using CNA_SalesWebMvc.Services;
using CNA_SalesWebMvc.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll(); // encontra todos os departamentos
            var viewModel = new SellerFormViewModel { Departments = departments }; // recebe a lista dos departamentos
            return View(viewModel); // envia para a view
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Redireciona para a interface index
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // instancia uma resposta que não encontrou o arquivo
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index)); // Redireciona para a interface index
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // instancia uma resposta que não encontrou o arquivo
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            // procura os departamento para povoar a lista de departamentos
            List<Department> departments = _departmentService.FindAll();

            // preenche o formulário com os dados que já existem o OBJ buscado e os Departamentos
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public IActionResult Edit(int id, Seller seller)
        {
            // o ID do vendedor que tá atualizando não pode ser diferente do da requisição
            if (id != seller.Id)
            {
                return BadRequest();
            }

            // pode gerar excessão na camada de serviço
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DBConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}