﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class CaseWhenField : ISqlField
    {
        public Dictionary<ConditionClause, ISqlField> Conditions { get; set; } = new Dictionary<ConditionClause, ISqlField>();

        public ISqlField Else { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
