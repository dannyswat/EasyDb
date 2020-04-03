using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDb.SqlMapper
{
    internal class TypeInfo
    {
        public PropertyInfo Property { get; set; }

        public bool Nullable { get; set; }
    }
}
