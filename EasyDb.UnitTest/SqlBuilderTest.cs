using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyDb.SqlBuilder;
using EasyDb.SqlBuilder.Components;

namespace EasyDb.UnitTest
{
    [TestClass]
    public class SqlBuilderTest
    {
        [TestMethod]
        public void SimpleQuery()
        {
            Query query = new Query();
            query.Select.Add(new SelectField("ID"));
            query.Select.Add(new SelectField("Name"));
            query.From = new Table("Products");
            query.Where = new ConditionClause(new Condition { Field1 = new DbField("Name"), Field2 = new Variable("name"), Operator = ComparisonOperator.Equal });
            query.OrderBy.Add(new SortField("Name"));

            ISqlGenerator sqlGenerator = new MsSqlGenerator();
            query.Accept(sqlGenerator);
            Assert.AreEqual("SELECT [ID], [Name] FROM [dbo].[Products] WHERE ([Name] = @name) ORDER BY [Name] ASC", sqlGenerator.GenerateSql());
        }
    }
}
