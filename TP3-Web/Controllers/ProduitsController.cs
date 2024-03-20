using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP3.Web.Data;
using TP3.Web.Models;
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
            CategorieAssociee = p.CategorieAssociee!,
            Fabricant = p.Fabricant,
            PrixEnCentime = p.PrixEnCentime,
            CreeLe = p.CreeLe
        }).OrderBy(p => p.CreeLe).ThenByDescending(p => p.PrixEnCentime).Take(20));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var produit = context.Produits.Include(produit => produit.CategorieAssociee)
            .Include(produit => produit.Fabricant).First(p => p.Id == id);

        return View(new ProduitDetailsVM
        {
            Nom = produit.Nom,
            Description = produit.Description,
            Categorie = produit.CategorieAssociee!,
            Fabricant = produit.Fabricant,
            Prix = produit.PrixEnCentime,
            CreeLe = produit.CreeLe
        });
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Produit produit;
        try
        {
            produit = context.Produits.Include(p => p.CategorieAssociee)
                .Include(p => p.Fabricant).First(p => p.Id == id);
        }
        catch (Exception e)
        {
            return NotFound();
        }

        return View(new ProduitEditVM
        {
            Id = produit.Id,
            Nom = produit.Nom,
            Description = produit.Description,
            CategorieAssocieeId = produit.CategorieId,
            CategorieAssociees = context.Categories
                .Select(c =>
                    new SelectListItem { Text = c.Titre, Value = c.Id.ToString() })
                .ToList(),
            FabricantId = produit.FabricantId,
            Fabricants = context.Fabricants
                .Select(f =>
                    new SelectListItem { Text = f.Nom, Value = f.Id.ToString() })
                .ToList(),
            PrixEnCentime = produit.PrixEnCentime,
            CreeLe = produit.CreeLe.ToString("yyyy-MM-dd HH:mm:ss")
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ProduitEditVM vm)
    {
        if (id != vm.Id)
        {
            return NotFound();
        }

        // Pour remplir les listes, vu que vm.CategoriesAssociees et vm.Fabricants est null ici
        vm.CategorieAssociees = context.Categories
            .Select(c =>
                new SelectListItem { Text = c.Titre, Value = c.Id.ToString() })
            .ToList();
        vm.Fabricants = context.Fabricants
            .Select(f =>
                new SelectListItem { Text = f.Nom, Value = f.Id.ToString() })
            .ToList();
        vm.CreeLe = context.Produits.Find(id).CreeLe.ToString("yyyy-MM-dd HH:mm:ss");

        ModelState.Remove("CategorieAssocieeId");
        
        if (ModelState.IsValid)
        {
            try
            {
                var produit = context.Produits.Find(vm.Id);
                produit.Nom = vm.Nom;
                produit.Description = vm.Description;
                produit.CategorieId = vm.CategorieAssocieeId;
                produit.FabricantId = vm.FabricantId;
                produit.PrixEnCentime = vm.PrixEnCentime;
                context.Update(produit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (context.Categories.Any(e => e.Id == vm.Id))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }
}