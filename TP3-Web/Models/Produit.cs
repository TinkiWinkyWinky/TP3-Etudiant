namespace TP3.Web.Models
{
    public class Produit
    {
        public int Id { get; set; }

        public string Nom { get; set; } = default!;

        public string Description { get; set; } = default!;

        public Categorie? CategorieAssociee { get; set; } = default!;

        public int? CategorieId { get; set; }

        public Fabricant Fabricant { get; set; } = default!;

        public int FabricantId { get; set; } = default!;

        public int PrixEnCentime { get; set; } = default!;

        public DateTime CreeLe { get; } = default!;
    }
}
