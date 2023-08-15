using PowerQualityManageService.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers.ExplicitMappings;
public static class DataSampleMapping
{
    public static Dictionary<string,object> ConvertToKeyValuePairs(this DataSamplesSQL dataSamplesSQL)
    {
        var data = new Dictionary<string, object?>();

        var properties = typeof(DataSamplesSQL).GetProperties().Where(x=> x.PropertyType == typeof(decimal?) || x.PropertyType == typeof(decimal));
        foreach (var property in properties)
        {
            //if (property.Name == "DataSample_Id" || property.Name == "DateTime" || property.Name == "Flagging" || property.Name == "MeasuringPoints_Id" || property.Name == "OTHERS") continue;
            var key = property.Name;
            var value = property.GetValue(dataSamplesSQL);
            if(value!= null) data.Add(key, value);
        }
        return data;
    }
    public static DataSample ToDataSample(this DataSamplesSQL dataSamplesSQL, string measuringPoint) 
    {
        return new DataSample()
        {
            Date = dataSamplesSQL.Date,
            Flagging = dataSamplesSQL.Flagging,
            MeasuringPoint = measuringPoint,
            Data = dataSamplesSQL.ConvertToKeyValuePairs()
        };
    }
}
