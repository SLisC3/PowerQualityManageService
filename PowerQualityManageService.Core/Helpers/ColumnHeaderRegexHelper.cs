using PowerQualityManageService.Core.Utils.Enums;
using PowerQualityManageService.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class ColumnHeaderRegexHelper
{
    public static IEnumerable<string> TrimQuotes(List<string> input)
    {
        return input.SetValues(x => x.Trim(new char[] { ' ', '\'', '"' }));
    }
}

public static class RegexConsts
{
    public const string Date = @"\bdate\b";
    public const string Time = @"\btime\b";
    public const string Flagging = @"\bflagging\b";
    public const string Frequency = "^f[^a-z]";
    public const string CosPhi = @"cos";
    public const string TanPhi = @"tan";
    public const string UnbalancedVoltage= @"unbalance.*voltage";
    public const string UnbalancedCurrent = @"unbalance.*current";

    public const string Voltage = @"^u{1}[l_]*(?=\d)";
    public const string Current = @"^i{1}[l_]*(?=\d)";
    public const string Power = @"^p{1}[_ ,-]+"; // TODO
    public const string Pst = @"pst";
    public const string Plt = @"plt";
    public const string ApparentPower = @"^s[_ ,-]+"; // TODO
    public const string QV = @"qv";
    public const string PF= @"pf";
    public const string THD = @"thd";
    public const string HarmonicVoltage = @"h{1}\d{1,2}[_, -]+";

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

    public const string Phase= @"[a-gi-z]{1}\d{1}(?!\d)"; // TODO
    public const string PhaseToPhase = @"[a-gi-z]{1}\d{2}"; // TODO
    public const string Neutral = @"neutral"; 
    public const string Total = @"total"; 

    public static Dictionary<string, TypeOfMeasurement> TypeOfMeasurementMap = new Dictionary<string, TypeOfMeasurement>()
    {
        {Phase, TypeOfMeasurement.Phase },
        {PhaseToPhase, TypeOfMeasurement.PhaseToPhase},
        {Neutral, TypeOfMeasurement.Neutral},
        {Total, TypeOfMeasurement.Total}
    };

    public const string Mean = @"mean"; // TODO
    public const string Min = @"min";
    public const string Max = @"max";
    public const string Abs = @"abs";

    public static Dictionary<string, TypeOfValue> TypeOfValueMap = new Dictionary<string, TypeOfValue>()
    {
        {Mean, TypeOfValue.Mean},
        {Min, TypeOfValue.Min},
        {Max, TypeOfValue.Max},
        {Abs, TypeOfValue.Abs}
    };
}
