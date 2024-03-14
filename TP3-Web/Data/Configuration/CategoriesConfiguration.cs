using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TP3.Web.Models;

namespace TP3.Web.Data.Configuration
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Categorie>
    {
        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder.HasData(new Categorie()
            {
                Id = 1,
                Titre = "Électronique",
                Description = "Tout ce qui touche l'électronique de près ou de loin.",
            },
            new Categorie()
            {
                Id = 2,
                Titre = "Cosméthiques et soins de beauté",
                Description = "Tout pour être plus ravissant!",
            },
            new Categorie()
            {
                Id = 3,
                Titre = "Sports",
                Description = "Ce qui permet de bouger plus!",
            },
            new Categorie()
            {
                Id = 4,
                Titre = "Gadgets",
                Description = "Tout ce qui peut être inutile.",
            });
        }
    }
}
