using TP3.Web.Models;

namespace TP3.Web.ViewModels
{
    public class DetailsCategoriesVM
    {
        public string Titre { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<Produit> ProduitsRelies { get; set; } = default!;
    }
}
