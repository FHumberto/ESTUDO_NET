using Microsoft.AspNetCore.Mvc;

namespace CNA_SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
