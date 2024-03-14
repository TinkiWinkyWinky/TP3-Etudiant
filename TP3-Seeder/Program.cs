using Bogus;
using TP3.Seeder;
using TP3.Web.Models;

Console.WriteLine("Début du seed!");
using var context = DbContextFactory.CreateDbContext();

// Ajoutés par migrations
context.RemoveRange(context.Produits);
context.RemoveRange(context.Fabricants);
context.SaveChanges();

// Générateur de dates
DateTime startCreation = new(2023, 1, 1);
int rangeCreation = (DateTime.Today - startCreation).Days;

// Création des fabricants
var fabricantsFaker = new Faker<Fabricant>();

fabricantsFaker
    .RuleFor(x => x.Nom, f => f.Company.CompanyName())
    .RuleFor(x => x.Adresse, f => string.Join(" ", f.Address.StreetAddress(), f.Address.StreetName()))
    .RuleFor(x => x.Ville, f => f.Address.City())
    .RuleFor(x => x.NombreEmployes, f => f.Random.Int(1, 1000))
    .FinishWith((f, x) =>
    {
        x.Id = 0; // Pour l'auto-incrément dans la BD
    });

var fabricants = fabricantsFaker.Generate(100);
context.AddRange(fabricants);
Console.WriteLine($"Sauvegarde des {nameof(Fabricant)} dans la BD");
context.SaveChanges();

var categories = context.Categories.ToList();
// Création des produits
var produitsFaker = new Faker<Produit>();
produitsFaker
    .RuleFor(x => x.CategorieAssociee, f => f.PickRandom(categories))
    .RuleFor(x => x.Nom, f => f.Commerce.ProductName())
    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
    .RuleFor(x => x.Fabricant, f => f.PickRandom(fabricants))
    .RuleFor(x => x.PrixEnCentime, f => f.Random.Int(100, 10_000))
    .FinishWith((f, x) =>
    {
        x.Id = 0; // Pour l'auto-incrément dans la BD
    });

context.AddRange(produitsFaker.Generate(5000));
Console.WriteLine("Sauvegarde des produits dans la BD");
context.SaveChanges();


