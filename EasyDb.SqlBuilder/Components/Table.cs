using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder.Components
{
    public class Table : ISqlTable
    {
        public Table() { }

        public Table(string table, string alias = null)
        {
            Name = table;
            Alias = alias;
        }

        public string Name { get; set; }

        public string Schema { get; set; }

        public string Alias { get; set; }

        public void Accept(ISqlComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
