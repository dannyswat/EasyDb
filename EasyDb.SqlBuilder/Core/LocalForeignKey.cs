using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public class LocalForeignKey : ISqlComponent
    {
        public LocalForeignKey(ISqlField local, ISqlField foreign)
        {
            LocalKey = local;
            ForeignKey = foreign;
        }

        public ISqlField LocalKey { get; set; }
        
        public ISqlField ForeignKey { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
