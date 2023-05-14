using PowerQualityManageService.Core.Utils.Enums;
using PowerQualityManageService.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;

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
    public Kind Kind { get { return _kind; } }

    private void Identify(string columnName)
    {
        (_kind, _kindValue) = HeaderHelper.DecodeColumn(columnName, ColumnHeaderRegexHelper.RegexConsts.KindMap);
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


public static class ColumnHeaderRegexHelper
{
    public static IEnumerable<string> TrimQuotes(List<string> input)
    {
        return input.SetValues(x => x.Trim(new char[] { ' ', '\'', '"' }));
    }

    public static class RegexConsts
    {
        private const string Date = @"\bdate\b";
        private const string Time = @"\btime\b";
        private const string Flagging = @"\bflagging\b";
        private const string Frequency = "^f[^a-z]";
        private const string CosPhi = @"cos";
        private const string TanPhi = @"tan";
        private const string UnbalancedVoltage = @"unbalance.*voltage";
        private const string UnbalancedCurrent = @"unbalance.*current";

        private const string Voltage = @"^u{1}[l_]*(?=\d|_)";
        private const string Current = @"^i{1}[l_]*(?=\d|_)";
        private const string Power = @"^p{1}[_ ,-]+"; 
        private const string Pst = @"pst";
        private const string Plt = @"plt";
        private const string ApparentPower = @"^s[_ ,-]+"; 
        private const string QV = @"qv";
        private const string PF = @"pf";
        private const string THD = @"thd";
        private const string HarmonicVoltage = @"h{1}\d{1,2}[_, -]+";

        public static Dictionary<string, Kind> KindMap = new Dictionary<string, Kind>()
    {
        {Date, Kind.Date },
        {Time, Kind.Time },
        {Flagging, Kind.Flagging},
        {Frequency, Kind.Frequency},
        {CosPhi, Kind.CosPhi},
        {TanPhi, Kind.TanPhi},
        {UnbalancedVoltage, Kind.UnbalancedVoltage},
        {UnbalancedCurrent, Kind.UnbalancedCurrent},
        {Voltage, Kind.Voltage},
        {Current, Kind.Current},
        {Power, Kind.Power},
        {Pst, Kind.ShortTermPercebility},
        {Plt, Kind.LongTermPercebility},
        {ApparentPower, Kind.ApparentPower},
        {QV, Kind.QV},
        {PF, Kind.PF},
        {THD, Kind.THD},
        {HarmonicVoltage, Kind.HarmonicVoltage}
    };

        private const string Phase = @"[a-gi-z]{1}\d{1}(?!\d)"; 
        private const string PhaseToPhase = @"[a-gi-z]{1}\d{2}"; 
        private const string Neutral = @"neutral";
        private const string Total = @"total";

        public static Dictionary<string, TypeOfMeasurement> TypeOfMeasurementMap = new Dictionary<string, TypeOfMeasurement>()
    {
        {Phase, TypeOfMeasurement.Phase },
        {PhaseToPhase, TypeOfMeasurement.PhaseToPhase},
        {Neutral, TypeOfMeasurement.Neutral},
        {Total, TypeOfMeasurement.Total}
    };

        private const string Mean = @"mean"; 
        private const string Min = @"min";
        private const string Max = @"max";
        private const string Abs = @"abs";

        public static Dictionary<string, TypeOfValue> TypeOfValueMap = new Dictionary<string, TypeOfValue>()
    {
        {Mean, TypeOfValue.Mean},
        {Min, TypeOfValue.Min},
        {Max, TypeOfValue.Max},
        {Abs, TypeOfValue.Abs}
    };
    }

}

public static class HeaderHelper
{
    public static Type GetType(byte val)
    {
        switch (val)
        {
            case 0:
                return typeof(Nullable);
            case 1:
            case 2:
                return typeof(DateTime);
            case 3:
                return typeof(string);
            default:
                return typeof(decimal);
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
        return (default(T), null)!;
    }
}


