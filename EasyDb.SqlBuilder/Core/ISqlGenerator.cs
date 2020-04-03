using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public interface ISqlGenerator : ISqlComponentVisitor
    {
        string GenerateSql();
    }
}
