﻿// <auto-generated />
using EasyDb.UnitTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyDb.UnitTest.Models
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyDb.UnitTest.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("prod_ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BaseUOM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("tblprod_Product");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            BaseUOM = "Piece",
                            Code = "001",
                            Name = "Product No.1"
                        },
                        new
                        {
                            ID = 2,
                            BaseUOM = "Piece",
                            Code = "002",
                            Name = "Product No.2"
                        });
                });

            modelBuilder.Entity("EasyDb.UnitTest.Models.Product", b =>
                {
                    b.OwnsOne("EasyDb.UnitTest.Models.Money", "Price", b1 =>
                        {
                            b1.Property<int>("ProductID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(3)")
                                .HasMaxLength(3);

                            b1.HasKey("ProductID");

                            b1.ToTable("tblprod_Product");

                            b1.WithOwner()
                                .HasForeignKey("ProductID");
                        });

                    b.OwnsMany("EasyDb.UnitTest.Models.ProductUOM", "UOMs", b1 =>
                        {
                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(15)")
                                .HasMaxLength(15);

                            b1.Property<int>("ProductID")
                                .HasColumnType("int");

                            b1.Property<decimal>("Multiple")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("Name", "ProductID");

                            b1.HasIndex("ProductID");

                            b1.ToTable("tblpuom_ProductUOM");

                            b1.WithOwner("Product")
                                .HasForeignKey("ProductID");

                            b1.HasData(
                                new
                                {
                                    Name = "Box",
                                    ProductID = 1,
                                    Multiple = 12m
                                },
                                new
                                {
                                    Name = "Box",
                                    ProductID = 2,
                                    Multiple = 12m
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
