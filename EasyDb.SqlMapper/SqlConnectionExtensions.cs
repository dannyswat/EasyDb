using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EasyDb.SqlMapper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Pending>")]
    public static class SqlConnectionExtensions
    {
        public static IEnumerable<T> Query<T>(this SqlConnection connection, string sql, object parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.AddParametersToCommand(parameters);

            return ReadRows<T>(cmd);
        }

        public static T Scalar<T>(this SqlConnection connection, string sql, object parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.AddParametersToCommand(parameters);

            return ReadScalar<T>(cmd);
        }

        public static int Execute(this SqlConnection connection, string sql, object parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.AddParametersToCommand(parameters);

            return Execute(cmd);
        }

        public static int StoredProcedure(this SqlConnection connection, string storedProcName, object parameters)
        {
            SqlCommand cmd = new SqlCommand(storedProcName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.AddParametersToCommand(parameters, allowArray: false);

            return Execute(cmd);
        }

        public static IEnumerable<T> StoredProcedure<T>(this SqlConnection connection, string storedProcName, object parameters)
        {
            SqlCommand cmd = new SqlCommand(storedProcName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.AddParametersToCommand(parameters, allowArray: false);

            return ReadRows<T>(cmd);
        }

        /// <summary>
        /// Add parameters by anonymous object supporting enumerable
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        public static void AddParametersToCommand(this SqlCommand cmd, object parameters, bool allowArray = true)
        {
            if (parameters == null) return;

            foreach (var prop in parameters.GetType().GetProperties())
            {
                if (TypeMapping.IsEnumerableType(prop.PropertyType))
                {
                    if (allowArray)
                    {
                        var array = ((IEnumerable)prop.GetValue(parameters)).Cast<object>().ToArray();

                        cmd.CommandText = cmd.CommandText.Replace("@" + prop.Name, "(" + string.Join(", ", Enumerable.Range(0, array.Length).Select(i => "@" + prop.Name + i.ToString())) + ")");

                        for (int i = 0; i < array.Length; i++)
                            cmd.Parameters.AddWithValue("@" + prop.Name + i.ToString(), array[i]);
                    }
                }
                else
                    cmd.Parameters.AddWithValue(prop.Name, prop.GetValue(parameters));
            }
        }

        static IEnumerable<T> ReadRows<T>(SqlCommand cmd)
        {
            var state = cmd.Connection.State;

            OpenConnection(cmd.Connection);

            List<T> list = new List<T>();
            Type t = typeof(T);
            var mapping = TypeMappingCache.Instance.GetOrAddMapping(t);

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    List<SqlColumn> columns = Enumerable.Range(0, reader.FieldCount).Select(i =>
                    new SqlColumn { Name = reader.GetName(i), SqlType = reader.GetFieldType(i) }).ToList();

                    while (reader.Read())
                    {
                        T obj = IfError(Activator.CreateInstance<T>, $"Mapping type {typeof(T).Name} must have a parameterless constructor");

                        for (int i = 0; i < columns.Count; i++)
                        {
                            string col = columns[i].Name;
                            var prop = mapping.FindByName(col);
                            if (prop != null)
                            {
                                string[] layers = col.Split('.');

                                if (!reader.IsDBNull(i))
                                {
                                    object tempObj = obj;
                                    string tempName = layers[0];
                                    for (int n = 0; n < layers.Length - 1; n++)
                                    {
                                        var tempProp = mapping.FindByName(tempName);
                                        var tempType = tempProp.Property.PropertyType;
                                        var newObj = tempProp.Property.GetValue(tempObj);
                                        if (newObj == null)
                                        {
                                            newObj = IfError(() => Activator.CreateInstance(tempType), $"Mapping sub-type {tempType.Name} must have a parameterless constructor");
                                            tempProp.Property.SetValue(tempObj, newObj);
                                        }
                                        tempObj = newObj;
                                        tempName += "." + layers[n + 1];
                                    }

                                    prop.Property.SetValue(tempObj, reader[col]);
                                }
                            }
                        }
                        list.Add(obj);
                    }
                }
            }
            finally
            {
                try
                {
                    if (state != ConnectionState.Open) // Keep connection open if the original state is open
                        cmd.Connection.Close();
                }
                catch { }
            }

            return list;
        }

        static int Execute(SqlCommand cmd)
        {
            var state = cmd.Connection.State;

            OpenConnection(cmd.Connection);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                try
                {
                    if (state != ConnectionState.Open) // Keep connection open if the original state is open
                        cmd.Connection.Close();
                }
                catch { }
            }
        }

        static T ReadScalar<T>(SqlCommand cmd)
        {
            var state = cmd.Connection.State;

            OpenConnection(cmd.Connection);

            try
            {
                var obj = cmd.ExecuteScalar();
                if (obj == null || Convert.IsDBNull(obj))
                    return default;
                return (T)obj;
            }
            finally
            {
                try
                {
                    if (state != ConnectionState.Open) // Keep connection open if the original state is open
                        cmd.Connection.Close();
                }
                catch { }
            }
        }

        static void OpenConnection(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Broken)
                conn.Close();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        static T IfError<T>(Func<T> func, string message)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                throw new Exception(message, e);
            }
        }
    }
}
