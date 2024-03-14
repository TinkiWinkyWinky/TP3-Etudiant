using Microsoft.EntityFrameworkCore;
using TP3.Web.Data.Configuration;
using TP3.Web.Models;

namespace TP3.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed les cours, car ce sont des données permanentes
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());

            modelBuilder.Entity<Produit>().Property(x => x.CreeLe).HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Categorie>().Property(x => x.DateCreation).HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Produit>()
                .HasOne(x => x.Fabricant)
                .WithMany(x => x.ProduitsAssocies)
                .HasForeignKey(x => x.FabricantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Categorie>()
                .HasMany(x => x.ProduitsRelies)
                .WithOne(x => x.CategorieAssociee)
                .HasForeignKey(x => x.CategorieId)
                .OnDelete(DeleteBehavior.SetNull);

        }

        public DbSet<Categorie> Categories { get; set; } = default!;

        public DbSet<Produit> Produits { get; set; } = default!;

        public DbSet<Fabricant> Fabricants { get; set; } = default!;

    }
}
