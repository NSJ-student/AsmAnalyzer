﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ArmAssembly
{
	public static class UserConvert
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string TableName)
		{
			DataTable dt = new DataTable(TableName);
			Type t = typeof(T);
			PropertyInfo[] pia = t.GetProperties();

			//Inspect the properties and create the columns in the DataTable
			foreach (PropertyInfo pi in pia)
			{
				Type ColumnType = pi.PropertyType;
				if ((ColumnType.IsGenericType))
				{
					ColumnType = ColumnType.GetGenericArguments()[0];
				}
				dt.Columns.Add(pi.Name, ColumnType);
			}

			//Populate the data table
			foreach (T item in collection)
			{
				DataRow dr = dt.NewRow();
				dr.BeginEdit();
				foreach (PropertyInfo pi in pia)
				{
					if (pi.GetValue(item, null) != null)
					{
						dr[pi.Name] = pi.GetValue(item, null);
					}
				}
				dr.EndEdit();
				dt.Rows.Add(dr);
			}
			return dt;
		}
	}
}
