using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Helpers;
public static class NormResultReportHelper
{
    public static IEnumerable<SingleNormDefinition> Norms = new List<SingleNormDefinition>()
    {
        new SingleNormDefinition("1", "Frequency", new NotSufficientSamples()),
        new SingleNormDefinition("2", new List<string>(){"Voltage_Phase1","Voltage_Phase2","Voltage_Phase3" }, new InRangePercentageComparison(230, 10, 95)),
        new SingleNormDefinition("3", new List<string>(){ "LongTermPercebility_PhaseToPhase12", "LongTermPercebility_PhaseToPhase23", "LongTermPercebility_PhaseToPhase31" }, new SimpleComparison(1, NormCalculationMethod.LesserThan,95)),
        new SingleNormDefinition("4a", new List<string>(){ "UnbalancedVoltage" }, new SimpleComparison(2, NormCalculationMethod.LesserThan,95)),
        new SingleNormDefinition("4b",new List<string>(){
            "HarmonicVoltage2_Phase1","HarmonicVoltage2_Phase2","HarmonicVoltage2_Phase3",
            "HarmonicVoltage3_Phase1","HarmonicVoltage3_Phase2","HarmonicVoltage3_Phase3",
            "HarmonicVoltage4_Phase1","HarmonicVoltage4_Phase2","HarmonicVoltage4_Phase3",
            "HarmonicVoltage5_Phase1","HarmonicVoltage5_Phase2","HarmonicVoltage5_Phase3",
            "HarmonicVoltage6_Phase1","HarmonicVoltage6_Phase2","HarmonicVoltage6_Phase3",
            "HarmonicVoltage7_Phase1","HarmonicVoltage7_Phase2","HarmonicVoltage7_Phase3",
            "HarmonicVoltage8_Phase1","HarmonicVoltage8_Phase2","HarmonicVoltage8_Phase3",
            "HarmonicVoltage9_Phase1","HarmonicVoltage9_Phase2","HarmonicVoltage9_Phase3",
            "HarmonicVoltage10_Phase1","HarmonicVoltage10_Phase2","HarmonicVoltage10_Phase3",
            "HarmonicVoltage11_Phase1","HarmonicVoltage11_Phase2","HarmonicVoltage11_Phase3",
            "HarmonicVoltage12_Phase1","HarmonicVoltage12_Phase2","HarmonicVoltage12_Phase3",
            "HarmonicVoltage13_Phase1","HarmonicVoltage13_Phase2","HarmonicVoltage13_Phase3",
            "HarmonicVoltage14_Phase1","HarmonicVoltage14_Phase2","HarmonicVoltage14_Phase3",
            "HarmonicVoltage15_Phase1","HarmonicVoltage15_Phase2","HarmonicVoltage15_Phase3",
            "HarmonicVoltage16_Phase1","HarmonicVoltage16_Phase2","HarmonicVoltage16_Phase3",
            "HarmonicVoltage17_Phase1","HarmonicVoltage17_Phase2","HarmonicVoltage17_Phase3",
            "HarmonicVoltage18_Phase1","HarmonicVoltage18_Phase2","HarmonicVoltage18_Phase3",
            "HarmonicVoltage19_Phase1","HarmonicVoltage19_Phase2","HarmonicVoltage19_Phase3",
            "HarmonicVoltage20_Phase1","HarmonicVoltage20_Phase2","HarmonicVoltage20_Phase3",
            "HarmonicVoltage21_Phase1","HarmonicVoltage21_Phase2","HarmonicVoltage21_Phase3",
            "HarmonicVoltage22_Phase1","HarmonicVoltage22_Phase2","HarmonicVoltage22_Phase3",
            "HarmonicVoltage23_Phase1","HarmonicVoltage23_Phase2","HarmonicVoltage23_Phase3",
            "HarmonicVoltage24_Phase1","HarmonicVoltage24_Phase2","HarmonicVoltage24_Phase3",
            "HarmonicVoltage25_Phase1","HarmonicVoltage25_Phase2","HarmonicVoltage25_Phase3"
        }, new Harmonic()),
        new SingleNormDefinition("5", new List<string>(){"THD_Phase1","THD_Phase2", "THD_Phase3" }, new SimpleComparison(8, NormCalculationMethod.LesserThan)),
        new SingleNormDefinition("6", new List<string>(){"TanPhi"}, new SimpleComparison(0.4m, NormCalculationMethod.LesserThan, 100))
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
