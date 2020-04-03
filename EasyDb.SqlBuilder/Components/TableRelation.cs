using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class TableRelation : TableJoin
    {
        public TableJoinType JoinType { get; set; }

        public List<LocalForeignKey> LocalForeignKeys { get; set; } = new List<LocalForeignKey>();

        public override void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
