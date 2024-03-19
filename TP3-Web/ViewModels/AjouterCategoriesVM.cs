using System.ComponentModel.DataAnnotations;
using TP3.Web.Models;

namespace TP3.Web.ViewModels
{
    public class AjouterCategoriesVM
    {
        [StringLength(30, ErrorMessage = "Le titre ne doit pas dépasser 30 caractères.")]
        public string Titre { get; set; } = default!;
        [StringLength(2000, ErrorMessage = "La description ne doit pas dépasser 2000 caractères.")]
        public string Description { get; set; } = default!;
    }
}
