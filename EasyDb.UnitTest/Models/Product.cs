using EasyDb.SqlBuilder.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyDb.UnitTest.Models
{
    [Table("tblprod_Product")]
    public class Product
    {
        [Key, Column("prod_ID")]
        public int ID { get; set; }

        [Required, StringLength(30)]
        public string Code { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public Money Price { get; set; }

        public string BaseUOM { get; set; }

        public List<ProductUOM> UOMs { get; set; }
    }

    [Table("tblpuom_ProductUOM")]
    public class ProductUOM
    {
        public int ProductID { get; set; }

        public Product Product { get; set; }

        [StringLength(15)]
        public string Name { get; set; }

        public decimal Multiple { get; set; }
    }
}
