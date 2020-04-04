using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class SortField : ISqlComponent
    {
        public SortField(string field, bool desc = false)
        {
            Field = new DbField(field);
            Descending = desc;
        }

        public ISqlField Field { get; set; }

        public bool Descending { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
