namespace TP3.Web.Models
{
    public class Fabricant
    {
        public int Id { get; set; }
        public string Nom { get; set; } = default!;
        public string Adresse { get; set; } = default!;

        public string Ville { get; set; } = default!;

        public int NombreEmployes { get; set; } = default!;

        public ICollection<Produit> ProduitsAssocies { get; set; } = default!;
    }
}
