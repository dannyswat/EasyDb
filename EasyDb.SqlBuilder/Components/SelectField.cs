using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class SelectField : ISqlComponent
    {
        public ISqlField Field { get; set; }

        public string Alias { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
