using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public interface ISqlTable : ISqlComponent
    {
        string Alias { get; set; }
    }
}
