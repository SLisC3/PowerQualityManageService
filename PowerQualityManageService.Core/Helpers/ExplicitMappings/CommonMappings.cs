using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers.ExplicitMappings;
public static class CommonMappings
{
    public static T MapDataRowToObject<T>(this DataRow dataRow) where T : new()
    {
        T obj = new T();
        foreach (DataColumn column in dataRow.Table.Columns)
        {
            PropertyInfo property = typeof(T).GetProperty(column.ColumnName);
            if (property != null && dataRow[column] != DBNull.Value)
            {
                object value = dataRow[column];
                if(property.Name== "Flagging") { property.SetValue(obj, !string.IsNullOrEmpty((string)value)); continue; }
                property.SetValue(obj, value);
            }
        }
        return obj;
    }
}
