using EasyDb.SqlBuilder.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using EasyDb.SqlMapper;
using EasyDb.SqlBuilder;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EasyDb.EFQuery
{
    public static class DbSetExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public static IEnumerable<TEntity> FindRecords<TEntity>(this DbSet<TEntity> dbSet, EntityQueryModel queryModel) where TEntity : class
        {
            var dbContext = dbSet.GetDbContext();
            IModel model = dbContext.Model;
            IEntityType entityType = model.FindEntityType(typeof(TEntity));
            IEnumerable<IPropertyBase> properties = entityType.GetPropertiesAndNavigations();
            string tableName = entityType.GetTableName();
            string fullTableName = entityType.GetFullTableName();

            List<SelectField> selectFields = new List<SelectField>();
            List<TableJoin> joinedTables = new List<TableJoin>();
            foreach (var field in properties)
            {
                switch (field)
                {
                    case Property p:
                        selectFields.Add(new SelectField(p.GetColumnName(), p.Name));
                        break;
                    case Navigation n:
                        var navType = n.GetTargetType();
                        if (navType.GetFullTableName() == fullTableName)
                        {
                            var navProps = navType.GetProperties();

                            foreach (var navField in navProps)
                            {
                                selectFields.Add(new SelectField(navField.GetColumnName(), n.Name + "." + navField.Name));
                            }
                        }
                        else
                        {
                        }
                        break;
                }
                
            }

            Query query = new Query();
            query.Select = selectFields;
            query.From = new Table(tableName, schema: entityType.GetSchema());
            ISqlGenerator sqlGenerator = new MsSqlGenerator();
            query.Accept(sqlGenerator);
            string sql = sqlGenerator.GenerateSql();
            return dbContext.MapQuery<TEntity>(sql, null);
        }

        public static IEnumerable<T> MapQuery<T>(this DbContext dbContext, string sql, object param)
        {
            return dbContext.Database.GetDbConnection().Query<T>(sql, param);
        }

        public static DbContext GetDbContext<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            var serviceProvider = dbSet.GetInfrastructure();
            var dbContext = serviceProvider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
            return dbContext.Context;
        }
    }
}
