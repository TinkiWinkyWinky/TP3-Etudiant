namespace TP3.Web.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Titre { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime DateCreation { get; } = default!;

        public ICollection<Produit> ProduitsRelies { get; set; } = default!;
    }
}
