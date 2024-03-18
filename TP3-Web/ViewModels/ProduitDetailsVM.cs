using TP3.Web.Models;

namespace TP3.Web.ViewModels;

public class ProduitDetailsVM
{
    public int Id { get; set; }
    
    public string Nom { get; set; }

    public string Description { get; set; }

    public Categorie Categorie { get; set; }

    public Fabricant Fabricant { get; set; }

    public int Prix { get; set; }

    public DateTime CreeLe { get; set; }
}