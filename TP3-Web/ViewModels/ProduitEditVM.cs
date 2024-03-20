using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TP3.Web.Models;

namespace TP3.Web.ViewModels;

public class ProduitEditVM
{
    public int Id { get; set; }
    
    [Required]
    public string Nom { get; set; }

    [Required]
    public string Description { get; set; }

    [DisplayName("Catégorie")]
    public List<SelectListItem>? CategorieAssociees { get; set; }
    public int? CategorieAssocieeId { get; set; }

    [DisplayName("Fabricant")]
    public List<SelectListItem>? Fabricants { get; set; }
    [Required]
    public int FabricantId { get; set; }

    [Required]
    [Range(100, 1000000, ErrorMessage = "Le prix doit se situer entre 100 $ et 1 000 000 $")]
    [DisplayName("Prix")]
    public int PrixEnCentime { get; set; }

    [DisplayName("Date de création")]
    public string? CreeLe { get; set; }
}