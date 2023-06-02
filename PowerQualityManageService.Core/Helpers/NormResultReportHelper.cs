using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Helpers;
public static class NormResultReportHelper
{
    public static IEnumerable<SingleNormDefinition> Norms = new List<SingleNormDefinition>() 
    { 
        new SingleNormDefinition("1", "Frequency", 100),
        new SingleNormDefinition("2", new List<string>(){"Voltage_Phase1","Voltage_Phase2","Voltage_Phase3" }, 100),
        new SingleNormDefinition("3", new List<string>(){ "LongTermPercebility_PhaseToPhase12", "LongTermPercebility_PhaseToPhase23", "LongTermPercebility_PhaseToPhase31" }, 100 ),
        new SingleNormDefinition("5", new List<string>(){"THD_Phase1","THD_Phase2", "THD_Phase3" }, 100),
    };

    public static IEnumerable<ChartDataDefinition> Charts = new List<ChartDataDefinition>()
    {
        new ChartDataDefinition("Napięcie średnie", new List<string>() { "Voltage_Phase1", "Voltage_Phase2", "Voltage_Phase3" } ),
        new ChartDataDefinition("Napięcie maksymalne", new List<string>() { "Voltage_Phase1_Max", "Voltage_Phase2_Max", "Voltage_Phase3_Max" } ),
        new ChartDataDefinition("Napięcie minimalne", new List<string>() { "Voltage_Phase1_Min", "Voltage_Phase2_Min", "Voltage_Phase3_Min" } )
    };

    public static List<string> GetKeys(IEnumerable<string>? initKeys)
    {
        List<string> keys = Charts.SelectMany(x => x.SamplesName).ToList();
        keys.AddRange(Norms.SelectMany(x => x.SamplesName));
        if(initKeys != null) keys.AddRange(initKeys);
        return keys.Distinct().ToList();
    }
}
