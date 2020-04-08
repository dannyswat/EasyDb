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

        [TestMethod]
        public void SimpleRowNumQuery()
        {
            Query query = new Query();
            query.Select.Add(new SelectField("ID"));
            query.Select.Add(new SelectField("Name"));
            query.Select.Add(new SelectField(new RowNumberField().Order(new SortField("ID")), "Row"));
            query.From = new Table("Products");
            query.Where = new ConditionClause(new Condition { Field1 = new DbField("Name"), Field2 = new Variable("name"), Operator = ComparisonOperator.Equal });
            query.OrderBy.Add(new SortField("Name"));

            ISqlGenerator sqlGenerator = new MsSqlGenerator();
            query.Accept(sqlGenerator);
            Assert.AreEqual("SELECT [ID], [Name], ROW_NUMBER() OVER (ORDER BY [ID] ASC) AS [Row] FROM [dbo].[Products] WHERE ([Name] = @name) ORDER BY [Name] ASC", sqlGenerator.GenerateSql());
        }

        [TestMethod]
        public void ConditionsTest()
        {
            Query query = new Query();
            query.Select.Add(new SelectField("ID"));
            query.Select.Add(new SelectField("Name"));
            query.From = new Table("Products");
            query.Where = new ConditionClause(new Condition { Field1 = new DbField("Name"), Field2 = new Variable("name"), Operator = ComparisonOperator.Equal });
            query.Where.OtherConditions.Add(new MoreCondition(LogicalOperator.And, new Condition(new DbField("ID"), ComparisonOperator.LessThan, new NumberField(5))));
            query.OrderBy.Add(new SortField("Name", true));

            ISqlGenerator sqlGenerator = new MsSqlGenerator();
            query.Accept(sqlGenerator);
            Assert.AreEqual("SELECT [ID], [Name] FROM [dbo].[Products] WHERE ([Name] = @name AND [ID] < 5) ORDER BY [Name] DESC", sqlGenerator.GenerateSql());
        }
    }
}
