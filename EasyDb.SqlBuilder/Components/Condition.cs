using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class Condition : ISqlCondition
    {
        public ISqlField Field1 { get; set; }

        public ISqlField Field2 { get; set; }

        public ComparisonOperator Operator { get; set; }

        public void CopyTo(Condition other)
        {
            other.Field1 = Field1;
            other.Field2 = Field2;
            other.Operator = Operator;
        }

        public virtual void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
