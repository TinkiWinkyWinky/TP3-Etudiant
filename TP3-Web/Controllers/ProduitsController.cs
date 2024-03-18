using Microsoft.AspNetCore.Mvc;
using TP3.Web.Data;
using TP3.Web.ViewModels;

namespace TP3.Web.Controllers;

public class ProduitsController(ApplicationDbContext context) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(context.Produits.Select(p => new ProduitIndexVM
        {
            Id = p.Id,
            Nom = p.Nom,
            Description = p.Description,
            CategorieAssociee = p.CategorieAssociee,
            Fabricant = p.Fabricant,
            PrixEnCentime = p.PrixEnCentime,
            CreeLe = p.CreeLe
        }).OrderBy(p => p.CreeLe).ThenByDescending(p => p.PrixEnCentime).Take(20));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {

        var produit = context.Produits.First(p => p.Id == id);
        Console.Write(produit);

        var vm = new ProduitDetailsVM
        {
            Nom = produit.Nom,
            Description = produit.Description,
            Categorie = context.Categories.Find(produit.CategorieId),
            Fabricant = context.Fabricants.Find(produit.FabricantId),
            Prix = produit.PrixEnCentime,
            CreeLe = produit.CreeLe
        };
        
        return View(vm);
    }

    /*[HttpPost]
    public IActionResult Edit(int id)
    {
        var produit = context.Produits.Find(id);
        
        return View(vm);
    }*/
    
}