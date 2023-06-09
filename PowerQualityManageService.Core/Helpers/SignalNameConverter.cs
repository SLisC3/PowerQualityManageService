using PowerQualityManageService.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class SignalNameConverter
{
    public static List<Dictionary<string,string>> ToListOfSimpleCharts(this List<ChartDataDefinition> charts)
    {
        List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
        foreach (ChartDataDefinition chart in charts)
        {
            output.Add(new Dictionary<string, string>() { { chart.Name, chart.SamplesName.First().ToSingleName() } });
        }
        return output;
    }
    public static string ToSingleName(this string name)
    {
        return Regex.Replace(name, @"[\d-]", string.Empty);
    }

    public static List<ChartDataDefinition> ToListOfAdvancedCharts(this List<Dictionary<string,string>> charts) 
    {
        List<ChartDataDefinition> output = new List<ChartDataDefinition>();
        foreach(Dictionary<string,string> chart in charts)
        {
            output.Add(new ChartDataDefinition(chart.Keys.First(), chart.Values.First().ToComboName()));
        }
        return output;
    }
    public static IEnumerable<string> ToComboName(this string name)
    {
        List<string> output = new List<string>();
        string TrimmedName = Regex.Replace(name, @"[\d-]", string.Empty);
        if (TrimmedName.IndexOf("phaseToPhase", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            output.Add(Regex.Replace(TrimmedName, @"PhaseToPhase", "PhaseToPhase12"));
            output.Add(Regex.Replace(TrimmedName, @"PhaseToPhase", "PhaseToPhase23"));
            output.Add(Regex.Replace(TrimmedName, @"PhaseToPhase", "PhaseToPhase31"));
        }
        else if (TrimmedName.IndexOf("phase", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            output.Add(Regex.Replace(TrimmedName, @"Phase", "Phase1"));
            output.Add(Regex.Replace(TrimmedName, @"Phase", "Phase2"));
            output.Add(Regex.Replace(TrimmedName, @"Phase", "Phase3"));
        }
        else { output.Add(name); }
        return output;
    }
}
