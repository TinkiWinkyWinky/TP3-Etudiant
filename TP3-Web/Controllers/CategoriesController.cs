using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TP3.Web.Data;
using TP3.Web.ViewModels;

namespace TP3.Web.Controllers
{
    public class CategoriesController(ApplicationDbContext _context) : Controller
    {
        public IActionResult Index()
        {
           return View(_context.Categories.Select(x => new ShowCategoriesVM()
            {
                Id = x.Id,
                Titre = x.Titre,
                Description = x.Description,
                NombreProduits = x.ProduitsRelies.Count(),
            }
            ));
        }

        public IActionResult Details(int id)
        {

            return View(_context.Categories.Where(x => x.Id == id).Select(x => new DetailsCategoriesVM()
            {
                Titre = x.Titre,
                Description = x.Description,
                ProduitsRelies = x.ProduitsRelies
            }).FirstOrDefault()
            );
        }
    }
}
