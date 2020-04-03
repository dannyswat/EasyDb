using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class RowNumberField : ISqlField
    {
        public List<SortField> PartitionBy { get; set; }

        public List<SortField> OrderBy { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
