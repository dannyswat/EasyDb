using EasyDb.SqlBuilder.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.SqlGenerators
{
    public class MySqlGenerator : ISqlGenerator
    {
        public string GenerateSql()
        {
            throw new NotImplementedException();
        }

        public void Visit(AggregateField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(ArrayField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(BetweenField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(Condition component)
        {
            throw new NotImplementedException();
        }

        public void Visit(ConditionClause component)
        {
            throw new NotImplementedException();
        }

        public void Visit(MoreCondition component)
        {
            throw new NotImplementedException();
        }

        public void Visit(CaseField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(CaseWhenField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(RowNumberField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(SelectField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(SortField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(Table component)
        {
            throw new NotImplementedException();
        }

        public void Visit(TableJoin component)
        {
            throw new NotImplementedException();
        }

        public void Visit(TableApply component)
        {
            throw new NotImplementedException();
        }

        public void Visit(TableRelation component)
        {
            throw new NotImplementedException();
        }

        public void Visit(Variable component)
        {
            throw new NotImplementedException();
        }

        public void Visit(SqlExpression component)
        {
            throw new NotImplementedException();
        }

        public void Visit(DbField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(StringField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(IsNullField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(CommonTableExpression component)
        {
            throw new NotImplementedException();
        }

        public void Visit(LocalForeignKey component)
        {
            throw new NotImplementedException();
        }

        public void Visit(Query component)
        {
            throw new NotImplementedException();
        }

        public void Visit(CompositeQuery component)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnionQuery component)
        {
            throw new NotImplementedException();
        }

        public void Visit(NumberField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(DateField component)
        {
            throw new NotImplementedException();
        }
    }
}
