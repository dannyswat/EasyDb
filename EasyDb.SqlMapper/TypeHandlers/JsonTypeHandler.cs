using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDb.SqlMapper.TypeHandlers
{
	public class JsonTypeHandler<TClr> : TypeHandler<TClr, string>
	{
		public JsonTypeHandler() : base(writeToDb, readFromDb)
		{
		}

		static string writeToDb(TClr obj)
		{
			if (obj == null)
				return null;
			return JsonConvert.SerializeObject(obj);
		}

		static TClr readFromDb(string json)
		{
			return JsonConvert.DeserializeObject<TClr>(json);
		}
	}
}
