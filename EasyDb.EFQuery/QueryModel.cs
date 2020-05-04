using EasyDb.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.EFQuery
{
    public class EntityQueryModel
    {
        public List<string> Includes { get; set; } = new List<string>();

        public List<QueryFilter> Filters { get; set; } = new List<QueryFilter>();

        public List<string> SortExpression { get; set; } = new List<string>();
    }

    public class QueryFilter
    {
        public string Property { get; set; }

        public ComparisonOperator Operator { get; set; }

        public object Value { get; set; }
    }
}
