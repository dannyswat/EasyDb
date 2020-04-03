using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public enum TableJoinType
    {
        InnerJoin, LeftJoin, RightJoin, FullJoin
    }

    public enum TableApplyType
    {
        CrossApply, OuterApply
    }
}
