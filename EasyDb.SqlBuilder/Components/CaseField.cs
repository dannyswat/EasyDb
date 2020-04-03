using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class CaseField : ISqlField
    {
        public ISqlField Field { get; set; }

        public Dictionary<ISqlField, ISqlField> When { get; set; } = new Dictionary<ISqlField, ISqlField>();

        public ISqlField Else { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
