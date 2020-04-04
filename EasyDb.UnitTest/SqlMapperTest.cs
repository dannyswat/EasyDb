using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyDb.SqlMapper;
using System.Data.SqlClient;
using System.Linq;

namespace EasyDb.UnitTest
{
    [TestClass]
    public class SqlMapperTest
    {
        [TestMethod]
        public void BasicTest()
        {
            SimpleObject[] result;
            using (SqlConnection conn = new SqlConnection(""))
            {
                result = conn.Query<SimpleObject>("SELECT ID, Name FROM Products", null).ToArray();
            }
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("Test", result[0].Name);
        }

        class SimpleObject
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }
    }
}
