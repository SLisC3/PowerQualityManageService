using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class LanguageHelper
{
    public static string MapToPl(string origin)
    {
        origin = origin.Replace("Phase", "Fazowe");
        origin = origin.Replace("PhaseToPhase", "Międzyfazowe");
        origin = origin.Replace("Total", "Całkowite");
        origin = origin.Replace("Neutral", "LiniiNeutralnej");
        origin = origin.Replace("Voltage", "Napięcie");
        origin = origin.Replace("Current", "Natężenie");
        origin = origin.Replace("Power", "Moc");
        origin = origin.Replace("ApparentPower", "MocPozorna");
        origin = origin.Replace("UnbalancedVoltage", "NiesymetryczneNapięcie");
        origin = origin.Replace("UnbalancedCurrent", "NiesymetryczneNatężenie");
        origin = origin.Replace("LongTermPercebilitty", "DługoterminowaPercepcyjność");
        origin = origin.Replace("ShortTermPercebilitty", "KrótkoterminowaPercepcyjność");
        return origin;
    }
    public static IEnumerable<string> MapToPlEnumerable(IEnumerable<string> origins)
    {
        var result = new List<string>();
        foreach(var origin in origins) 
        {
            result.Add(MapToPl(origin)); 
        }
        return result;
    }

}
