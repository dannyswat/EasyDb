using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlMapper
{
    public class TypeHandlerManager
    {
        Dictionary<Type, HandlerInfo> handlers = new Dictionary<Type, HandlerInfo>();

        internal TypeHandlerManager() { }

        public static void Add<TClr, TDb>(TypeHandler<TClr, TDb> handler)
        {
            SqlConnectionExtensions.TypeHandler.AddTypeHandler(handler);
        }

        public void AddTypeHandler<TClr, TDb>(TypeHandler<TClr, TDb> handler)
        {
            if (!handlers.ContainsKey(typeof(TClr)))
                handlers.Add(typeof(TClr), new HandlerInfo { DbType = typeof(TDb), Handler = handler });
        }

        internal HandlerInfo FindHandler(Type t)
        {
            if (handlers.ContainsKey(t))
                return handlers[t];
            return null;
        }

        internal class HandlerInfo
        {
            public Type DbType { get; set; }

            public ITypeHandler Handler { get; set; }
        }
    }
}
