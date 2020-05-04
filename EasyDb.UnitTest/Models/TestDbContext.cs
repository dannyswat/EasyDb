using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.UnitTest.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() : base() { }
        public TestDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=EasyDb; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .OwnsOne(t => t.Price, (builder) =>
                {
                    builder.WithOwner();
                })
                .OwnsMany(t => t.UOMs, (f) =>
                {
                    f.WithOwner(t => t.Product).HasForeignKey(t => t.ProductID);
                    f.HasKey(t => new { t.Name, t.ProductID });
                    f.HasData(
                        new ProductUOM { Name = "Box", Multiple = 12, ProductID = 1 },
                        new ProductUOM { Name = "Box", Multiple = 12, ProductID = 2 }
                        );
                })
                .HasData(
                    new Product { ID = 1, Code = "001", Name = "Product No.1"
                    , BaseUOM = "Piece"
                        //, Price = new Money("HKD", 99) 
                    },
                    new Product { ID = 2, Code = "002", Name = "Product No.2"
                    , BaseUOM = "Piece"
                    //, Price = new Money("HKD", 199) 
                    }
                );
        }
    }
}
