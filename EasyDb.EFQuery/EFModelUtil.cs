using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.EFQuery
{
    public static class EFModelUtil
    {
        public static string GetFullTableName(this IEntityType entityType)
        {
            string schema = entityType.GetSchema();
            return (string.IsNullOrEmpty(schema) ? "" : schema + ".") + entityType.GetTableName();
        }
    }
}
