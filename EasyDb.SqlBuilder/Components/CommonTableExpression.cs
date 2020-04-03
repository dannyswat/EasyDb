using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class CommonTableExpression : Query
    {
        public string Name { get; set; }

        public override void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
