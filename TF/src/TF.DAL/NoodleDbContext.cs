using System.Data.Entity;
using TF.Data.Business;
using TF.Data.Business.WMS;

namespace TF.DAL
{
    class NoodleDbContext : DbContext
    {
        public NoodleDbContext() : base("NoodleDb") { }
        public NoodleDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("[BUSINESS.WMS.PRODUCT_N_SERVICE]");
            modelBuilder.Entity<Product>().HasKey(r => r.Id);
            modelBuilder.Entity<Product>().Property(r => r.Id)
                .HasColumnName("GUID_RECORD")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>().Property(r => r.Name).HasColumnName("NAME").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Product>().Property(r => r.Key).HasColumnName("KEY").IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(r => r.Type).HasColumnName("TYPE").IsRequired().HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .HasMany(r => r.ChildProducts)
                .WithRequired(r => r.Parent);

        }
    }
}
