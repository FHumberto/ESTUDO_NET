using CNA_SalesWebMvc.Models;
using CNA_SalesWebMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNA_SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        // injeção de dependencia
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // indica que a ação é de Post
        [ValidateAntiForgeryToken] //prefine ataque CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index)); // Redireciona para a interface index
        }
    }
}