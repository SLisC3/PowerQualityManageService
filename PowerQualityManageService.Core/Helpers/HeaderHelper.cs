using PowerQualityManageService.Core.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class HeaderHelper
{
    public static Type GetType (byte val)
    {
        switch (val)
        {
            case 0:
                return typeof(Nullable);
            case 1:
            case 2:
                return typeof (DateTime);
            case 3:
                return typeof (bool);
            default: 
                return typeof (decimal);
        }
    }
    public static (T, string?) DecodeColumn<T>(string columnName, Dictionary<string, T> enumMapping) where T : Enum
    {
        foreach (var mapping in enumMapping)
        {
            Match match = Regex.Match(columnName, mapping.Key, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var numericValue = new String(match.Value.Where(Char.IsDigit).ToArray());
                return (mapping.Value, numericValue);
            }
        }
        return (default(T), null);
    }
}

public class ColumnHeader
{
    private Kind _kind;
    private string? _kindValue;
    private TypeOfMeasurement _typeOfMeasurement;
    private string? _typeOfMeasurementValue;
    private TypeOfValue _typeOfValue;
    private string _name;
    private Type _type;
    private string _columnName;  // TODO : For now only for debugging

    public ColumnHeader(string columnName)
    {
        _columnName = columnName;
        Identify(columnName);

        _name = GenerateColumnName();
        _type = HeaderHelper.GetType((byte)_kind);
    }

    public string Name { get { return _name; } }
    public Type VariableType { get { return _type; } }

    private void Identify(string columnName)
    {
        (_kind,_kindValue) = HeaderHelper.DecodeColumn(columnName, ColumnHeaderRegexHelper.RegexConsts.KindMap);
        (_typeOfMeasurement, _typeOfMeasurementValue) = HeaderHelper.DecodeColumn(columnName, ColumnHeaderRegexHelper.RegexConsts.TypeOfMeasurementMap);
        (_typeOfValue, _) = HeaderHelper.DecodeColumn(columnName, ColumnHeaderRegexHelper.RegexConsts.TypeOfValueMap);
    }
    public string GenerateColumnName()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(_kind.ToString());
        if (_kind == Kind.Unrecognized) return sb.ToString();
        sb.Append(_kindValue ?? string.Empty);
        if (_typeOfMeasurement != TypeOfMeasurement.NotApplicable)
        {
            sb.Append("_");
            sb.Append(_typeOfMeasurement.ToString());
            sb.Append(_typeOfMeasurementValue ?? string.Empty);

        }
        if (_typeOfValue != TypeOfValue.NotApplicable)
        {
            sb.Append("_");
            sb.Append(_typeOfValue);
        }
        return sb.ToString();
    }
}
