using CodingWiki_DataAcess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers;
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<Category> objList = _db.Categoiries.ToList();
        return View(objList);
    }
}
