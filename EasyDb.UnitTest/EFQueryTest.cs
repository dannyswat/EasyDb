using EasyDb.UnitTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyDb.EFQuery;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using EasyDb.SqlBuilder;

namespace EasyDb.UnitTest
{
    [TestClass]
    public class EFQueryTest
    {
        public EFQueryTest()
        {
        }

        [TestInitialize]
        public void InitializeDatabase()
        {
            using (var db = new TestDbContext())
            {
                db.Database.Migrate();
            }
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var db = new TestDbContext())
            {
                Assert.AreEqual(2, db.Products.Count());
                var products = db.Products.FindRecords(null);
                Assert.AreEqual(2, products.Count());
                Assert.AreEqual(1, products.First().ID);
                Assert.AreEqual(99, products.First().Price?.Amount);
            }
        }

        [TestMethod]
        public void TestQuery2()
        {
            using (var db = new TestDbContext())
            {
                Assert.AreEqual(2, db.Products.Count());
                var products = db.Products.FindRecords(null);
                Assert.AreEqual(2, products.Count());
                Assert.AreEqual(1, products.First().ID);
                Assert.AreEqual(99, products.First().Price?.Amount);
            }
        }

        [TestMethod]
        public void TestQueryEfCompare()
        {
            using (var db = new TestDbContext())
            {
                Assert.AreEqual(2, db.Products.Count());
                var products = db.Products.ToList();
                Assert.AreEqual(2, products.Count());
                Assert.AreEqual(1, products.First().ID);
                Assert.AreEqual(99, products.First().Price?.Amount);
            }
        }
    }
}
