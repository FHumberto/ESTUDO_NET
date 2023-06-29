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

    public IActionResult Upsert(int? id)
    {
        Category obj = new();
        if(id == null || id == 0)
        {
            // create
            return View(obj);
        }
        //edit
        obj = _db.Categoiries.First(u => u.CategoryId == id);
        if(obj == null)
        {
            return NotFound();
        }
        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Category obj)
    {
        if (ModelState.IsValid)
        {
            //create
            await _db.Categoiries.AddAsync(obj);
        }
        else
        {
            //update
            _db.Categoiries.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(obj);
    }
}
