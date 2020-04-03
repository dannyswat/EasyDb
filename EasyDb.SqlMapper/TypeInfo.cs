using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDb.SqlMapper
{
    public class TypeInfo
    {
        public PropertyInfo Property { get; set; }

        public bool Nullable { get; set; }
    }
}
