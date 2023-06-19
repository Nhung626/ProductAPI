using Microsoft.EntityFrameworkCore;
using ProductAPI.Enities;
namespace ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
               entity.ToTable("Product");
               entity.HasKey(e => e.Id);
               entity.Property(e => e.Id)
                     .ValueGeneratedOnAdd();
               entity.Property(e => e.ProductName)
                   .IsUnicode()
                   .IsRequired();
               entity.Property(e => e.Status)
                   .IsUnicode()
                   .IsRequired();
                entity.Property(e => e.Price)
                   .IsRequired();
               
            });
            //cách 2: dùng các attribute annotations sẽ viết bên trong các class entity
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //  optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Product;Trusted_Connection=True;");
        //}
    }
}