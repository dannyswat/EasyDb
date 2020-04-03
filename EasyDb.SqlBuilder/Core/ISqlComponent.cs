using System;

namespace EasyDb.SqlBuilder
{
    public interface ISqlComponent
    {
        void Accept(ISqlComponentVisitor visitor);
    }
}
