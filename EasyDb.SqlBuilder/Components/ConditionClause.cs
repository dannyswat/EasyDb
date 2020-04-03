using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class ConditionClause : ISqlCondition
    {
        public ConditionClause(Condition condition)
        {
            Condition = condition;
        }

        public ISqlCondition Condition { get; set; }

        public List<MoreCondition> OtherConditions { get; set; } = new List<MoreCondition>();

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
