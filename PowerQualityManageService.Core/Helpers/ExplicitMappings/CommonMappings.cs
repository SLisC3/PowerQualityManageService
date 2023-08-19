using AutoMapper;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Model.Models;
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

    public static IEnumerable<DataSamplesSQL_Header> MapListDataSampleToHeader(this IEnumerable<DataSampleId> dataSamples, int measuringPointId)
    {
        var configExpression = new MapperConfigurationExpression();
        configExpression.AddProfile<DataSampleProfile>();
        var config = new MapperConfiguration(configExpression);
        var mapper = new Mapper(config);
        var result = mapper.Map<IEnumerable<DataSamplesSQL_Header>>(dataSamples);
        foreach (var item in result)
        {
            item.MeasuringPoints_Id= measuringPointId;
        }
        return result;
    }
}
