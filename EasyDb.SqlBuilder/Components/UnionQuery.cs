using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class UnionQuery : ISqlTable
    {
        public bool UnionAll { get; set; }

        public Query Query { get; set; }

        string ISqlTable.Alias { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
