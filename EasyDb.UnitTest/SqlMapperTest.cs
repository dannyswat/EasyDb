using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyDb.SqlMapper;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace EasyDb.UnitTest
{
    [TestClass]
    public class SqlMapperTest
    {
        static string ConnectionString = "Data Source=TestDb;Mode=Memory;Cache=Shared";

        SqliteConnection conn;

        [TestInitialize]
        public void ConfigureDatabase()
        {
            conn = new SqliteConnection(ConnectionString);
            conn.Open();
            conn.Execute(@"CREATE TABLE Products (
ID INTEGER PRIMARY KEY,
Name TEXT NOT NULL)", null);

            conn.Execute(@"INSERT INTO Products (ID, Name)
Values (1, 'Test')", null);
        }

        [TestMethod]
        public void BasicTest()
        {
            SimpleObject[] result;

            result = conn.Query<SimpleObject>("SELECT ID, Name FROM Products", null).ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Test", result[0].Name);

            conn.Close();
        }

        [TestMethod]
        public void TwoLevelObjectTest()
        {
            SimpleObject[] result;

            result = conn.Query<SimpleObject>("SELECT ID, Name, Name AS [Detail.Name] FROM Products", null).ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Test", result[0].Name);
            Assert.AreEqual("Test", result[0].Detail.Name);

            conn.Close();
        }

        class SimpleObject
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public SubObject Detail { get; set; }
        }

        class SubObject
        {
            public string Name { get; set; }
        }
    }
}
