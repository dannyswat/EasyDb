using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDb.SqlMapper
{
    public interface ITypeHandler
    {
        void SetParameterValue(IDbDataParameter param, object value);

        object ReadValue(object dbValue);
    }
}
