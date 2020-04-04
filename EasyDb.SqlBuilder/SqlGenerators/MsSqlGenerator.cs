using EasyDb.SqlBuilder.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public class MsSqlGenerator : ISqlGenerator
    {
        StringBuilder sql = new StringBuilder();

        public string DefaultTableSchema { get; set; } = "dbo";

        public static string TranslateComparisonOperator(ComparisonOperator comparisonOperator)
        {
            switch (comparisonOperator)
            {
                case ComparisonOperator.Between:
                    return "BETWEEN";
                case ComparisonOperator.Equal:
                    return "=";
                case ComparisonOperator.Exists:
                    return "EXISTS";
                case ComparisonOperator.GreaterThan:
                    return ">";
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return ">=";
                case ComparisonOperator.In:
                    return "IN";
                case ComparisonOperator.IsNotNull:
                    return "IS NOT NULL";
                case ComparisonOperator.IsNull:
                    return "IS NULL";
                case ComparisonOperator.LessThan:
                    return "<";
                case ComparisonOperator.LessThanOrEqualTo:
                    return "<=";
                case ComparisonOperator.Like:
                    return "LIKE";
                case ComparisonOperator.NotBetween:
                    return "NOT BETWEEN";
                case ComparisonOperator.NotEqual:
                    return "<>";
                case ComparisonOperator.NotExists:
                    return "NOT EXISTS";
                case ComparisonOperator.NotIn:
                    return "NOT IN";
                case ComparisonOperator.NotLike:
                    return "NOT LIKE";
                default:
                    throw new NotImplementedException($"Operator {Enum.GetName(typeof(ComparisonOperator), comparisonOperator)} is not supported yet");
            }
        }

        public static string TranslateJoinType(TableJoinType joinType)
        {
            switch (joinType)
            {
                case TableJoinType.LeftJoin:
                    return "LEFT JOIN";
                case TableJoinType.InnerJoin:
                    return "INNER JOIN";
                case TableJoinType.RightJoin:
                    return "RIGHT JOIN";
                case TableJoinType.FullJoin:
                    return "FULL JOIN";
                default:
                    throw new NotImplementedException($"Unsupported join type {Enum.GetName(typeof(TableJoinType), joinType)}");
            }
        }

        public string GenerateSql()
        {
            return sql.ToString();
        }

        public void Visit(AggregateField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(ArrayField component)
        {
            sql.Append("(" + string.Join(", ", component.Data) + ")");
        }

        public void Visit(BetweenField component)
        {
            component.From.Accept(this);
            sql.Append(" AND ");
            component.To.Accept(this);
        }

        public void Visit(Condition component)
        {
            switch (component.Operator)
            {
                case ComparisonOperator.Exists:
                case ComparisonOperator.NotExists:

                    if (!(component.Field1 is Query))
                        throw new InvalidOperationException("Field1 must be a query for EXISTS operator");

                    sql.Append(component.Operator == ComparisonOperator.NotExists ? "NOT EXISTS (" : "EXISTS (");
                    component.Field1.Accept(this);
                    sql.Append(")");
                    break;
                case ComparisonOperator.IsNull:
                case ComparisonOperator.IsNotNull:
                    component.Field1.Accept(this);
                    sql.Append($" {TranslateComparisonOperator(component.Operator)}");
                    break;
                default:
                    component.Field1.Accept(this);
                    sql.Append($" {TranslateComparisonOperator(component.Operator)} ");
                    component.Field2.Accept(this);
                    break;
            }
        }

        public void Visit(ConditionClause component)
        {
            sql.Append("(");
            component.Condition.Accept(this);

            foreach (var condition in component.OtherConditions)
            {
                condition.Accept(this);
            }
            sql.Append(")");
        }

        public void Visit(MoreCondition component)
        {
            sql.Append(component.LogicalOperator == LogicalOperator.And ? " AND " : " OR ");
            component.Condition.Accept(this);
        }

        public void Visit(CaseField component)
        {
            sql.Append("(CASE ");
            component.Field.Accept(this);
            foreach (var fieldCase in component.When)
            {
                sql.Append(" WHEN ");
                fieldCase.Key.Accept(this);
                sql.Append(" THEN ");
                fieldCase.Value.Accept(this);
            }

            if (component.Else != null)
            {
                sql.Append(" ELSE ");
                component.Else.Accept(this);
            }
            sql.Append(" END)");
        }

        public void Visit(CaseWhenField component)
        {
            sql.Append("(CASE");
            foreach (var conditionValue in component.Conditions)
            {
                sql.Append(" WHEN ");
                conditionValue.Key.Accept(this);
                sql.Append(" THEN ");
                conditionValue.Value.Accept(this);
            }

            if (component.Else != null)
            {
                sql.Append(" ELSE ");
                component.Else.Accept(this);
            }
            sql.Append(" END)");
        }

        public void Visit(RowNumberField component)
        {
            throw new NotImplementedException();
        }

        public void Visit(SelectField component)
        {
            component.Field.Accept(this);
            if (!string.IsNullOrEmpty(component.Alias))
                sql.Append($" AS [{component.Alias}]");
        }

        public void Visit(SortField component)
        {
            component.Field.Accept(this);
            sql.Append(component.Descending ? " DESC" : " ASC");
        }

        public void Visit(Table component)
        {
            sql.Append($"[{component.Schema ?? DefaultTableSchema}].[{component.Name}]");
            if (!string.IsNullOrEmpty(component.Alias))
                sql.Append(" AS [" + component.Alias + "]");
        }

        public void Visit(TableJoin component)
        {
            sql.Append(" CROSS JOIN ");
            component.ForeignTable.Accept(this);
        }

        public void Visit(TableApply component)
        {
            sql.Append($" {(component.JoinType == TableApplyType.CrossApply ? "CROSS APPLY" : "OUTER APPLY")} (");
            component.ForeignTable.Accept(this);
            sql.Append($") AS [{((ISqlTable)component.ForeignTable).Alias}]");
        }

        public void Visit(TableRelation component)
        {
            sql.Append(" " + TranslateJoinType(component.JoinType) + " ");
            component.ForeignTable.Accept(this);
            sql.Append(" ON ");
            component.LocalForeignKeys.First().Accept(this);
            foreach (var key in component.LocalForeignKeys.Skip(1))
            {
                sql.Append(" AND ");
                key.Accept(this);
            }

        }

        public void Visit(Variable component)
        {
            sql.Append("@" + component.Name);
        }

        public void Visit(SqlExpression component)
        {
            sql.Append(component.Expression);
        }

        public void Visit(DbField component)
        {
            if (!string.IsNullOrEmpty(component.TableName))
                sql.Append($"[{component.TableName}].[{component.FieldName}]");
            else
                sql.Append($"[{component.FieldName}]");
        }

        public void Visit(StringField component)
        {
            sql.Append("N'" + component.Value + "'");
        }

        public void Visit(IsNullField component)
        {
            sql.Append("ISNULL(");
            component.Field.Accept(this);
            sql.Append(", ");
            component.WhenNull.Accept(this);
            sql.Append(")");
        }

        public void Visit(CommonTableExpression component)
        {
            throw new NotImplementedException();
        }

        public void Visit(LocalForeignKey component)
        {
            component.LocalKey.Accept(this);
            sql.Append(" = ");
            component.ForeignKey.Accept(this);
        }

        public void Visit(Query component)
        {

            sql.Append("SELECT ");

            if (component.Select.Count == 0)
            {
                sql.Append("*");
            }
            else
            {
                for (int i = 0; i < component.Select.Count; i++)
                {
                    if (i > 0) sql.Append(", ");
                    component.Select[i].Accept(this);
                }
            }

            if (component.From != null)
            {
                sql.Append(" FROM ");

                component.From.Accept(this);

                for (int i = 0; i < component.JoinTables.Count; i++)
                {
                    component.JoinTables[i].Accept(this);
                }
            }

            if (component.Where != null)
            {
                sql.Append(" WHERE ");

                component.Where.Accept(this);
            }

            if (component.GroupBy.Count > 0)
            {
                sql.Append(" GROUP BY ");

                for (int i = 0; i < component.GroupBy.Count; i++)
                {
                    if (i > 0) sql.Append(", ");
                    component.GroupBy[i].Accept(this);
                }
            }

            if (component.Having != null)
            {
                sql.Append(" HAVING ");

                component.Having.Accept(this);
            }

            if (component.OrderBy.Count > 0)
            {
                sql.Append(" ORDER BY ");

                for (int i = 0; i < component.OrderBy.Count; i++)
                {
                    if (i > 0) sql.Append(", ");
                    component.OrderBy[i].Accept(this);
                }
            }
        }

        public void Visit(CompositeQuery component)
        {
            throw new NotImplementedException();
        }

        public void Visit(UnionQuery component)
        {
            throw new NotImplementedException();
        }
    }
}
