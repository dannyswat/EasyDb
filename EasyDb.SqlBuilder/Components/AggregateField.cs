using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class AggregateField : ISqlField
    {
        public AggregateFunction Function { get; set; }

        public bool IsDistinct { get; set; }

        public List<SortField> PartitionBy { get; set; }

        public List<SortField> OrderBy { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
