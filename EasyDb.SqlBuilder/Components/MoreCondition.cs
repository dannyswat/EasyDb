using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class MoreCondition : ISqlComponent
    {
        public MoreCondition(LogicalOperator logicalOperator, ISqlCondition condition)
        {
            LogicalOperator = logicalOperator;
            Condition = condition;
        }

        public LogicalOperator LogicalOperator { get; set; }

        public ISqlCondition Condition { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
