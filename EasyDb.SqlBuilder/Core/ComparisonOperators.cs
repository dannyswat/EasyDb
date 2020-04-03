using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlBuilder
{
    public enum ComparisonOperator
    {
        Equal, 
        NotEqual, 
        GreaterThan, 
        LessThan, 
        GreaterThanOrEqualTo, 
        LessThanOrEqualTo, 
        Like,
        NotLike,
        Between,
        NotBetween,
        In, 
        NotIn,
        Exists,
        NotExists,
        IsNull,
        IsNotNull
    }
}
