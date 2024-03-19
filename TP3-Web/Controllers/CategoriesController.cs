using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TP3.Web.Data;
using TP3.Web.Models;
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

        public IActionResult Supprimer(int id)
        {
            var categorie = _context.Categories.Find(id);

            _context.Categories.Remove(categorie);
            _context.SaveChanges();

            var produitsAssocies = _context.Produits.Where(p => p.CategorieId == id);
            foreach (var produit in produitsAssocies)
            {
                produit.CategorieId = null;
            }
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Suppression effectuée avec succès.";
            return RedirectToAction("Index");
        }

        public IActionResult Ajouter()
        {
            var ajouterCategoriesVM = new AjouterCategoriesVM();

            return View(ajouterCategoriesVM);
        }

        [HttpPost]
        public IActionResult Ajouter(AjouterCategoriesVM ajouterCategoriesVM)
        {
            if (ModelState.IsValid)
            {
                if (_context.Categories.Any(c => c.Titre == ajouterCategoriesVM.Titre))
                {
                    ModelState.AddModelError("Titre", "Ce titre existe déjà.");
                    return View(ajouterCategoriesVM);
                }

                var categorie = new Categorie
                {
                    Titre = ajouterCategoriesVM.Titre,
                    Description = ajouterCategoriesVM.Description
                };

                _context.Categories.Add(categorie);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Création effectuée avec succès.";
                return RedirectToAction("Index");
            }
            return View(ajouterCategoriesVM);
        }
    }
}
