using System.ComponentModel.DataAnnotations;

namespace PowerQualityManageService.Model.Models;

public class TemplateSQL
{
    [Key]
    public int Template_Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Charts { get; set; } = null!;
}


public class ReportSQL
{
    [Key]
    public int Report_Id { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Data { get; set; } = null!;
    public int REP_MeasuringPoint_Id { get; set; }
    public int REP_Template_Id { get; set; }
}

public class MeasuringPointSQL
{
    [Key]
    public int MeasuringPoint_Id { get; set; }
    public string Name { get; set; } = null!;
}

public class DataSamplesSQL 
{
    [Key]
    public int DataSample_Id { get; set; }
    public DateTime Date { get; set; }
    public bool Flagging { get; set; }
    public int MeasuringPoints_Id { get; set; }
    public decimal? Voltage_PhaseToPhase12 { get; set; }
    public decimal? Voltage_PhaseToPhase23 { get; set; }
    public decimal? Voltage_PhaseToPhase31 { get; set; }
    public decimal? Voltage_PhaseToPhase12_Max { get; set; }
    public decimal? Voltage_PhaseToPhase23_Max { get; set; }
    public decimal? Voltage_PhaseToPhase31_Max { get; set; }
    public decimal? Voltage_PhaseToPhase12_Min { get; set; }
    public decimal? Voltage_PhaseToPhase23_Min { get; set; }
    public decimal? Voltage_PhaseToPhase31_Min { get; set; }
    public decimal? Current_Phase1 { get; set; }
    public decimal? Current_Phase2 { get; set; }
    public decimal? Current_Phase3 { get; set; }
    public decimal? Current_Neutral { get; set; }
    public decimal? Current_Phase1_Max { get; set; }
    public decimal? Current_Phase2_Max { get; set; }
    public decimal? Current_Phase3_Max { get; set; }
    public decimal? Current_Neutral_Max { get; set; }
    public decimal? Current_Phase1_Min { get; set; }
    public decimal? Current_Phase2_Min { get; set; }
    public decimal? Current_Phase3_Min { get; set; }
    public decimal? Current_Neutral_Min { get; set; }
    public decimal? ShortTermPercebility_PhaseToPhase12 { get; set; }
    public decimal? ShortTermPercebility_PhaseToPhase23 { get; set; }
    public decimal? ShortTermPercebility_PhaseToPhase31 { get; set; }
    public decimal? LongTermPercebility_PhaseToPhase12 { get; set; }
    public decimal? LongTermPercebility_PhaseToPhase23 { get; set; }
    public decimal? LongTermPercebility_PhaseToPhase31 { get; set; }
    public decimal? UnbalancedVoltage { get; set; }
    public decimal? UnbalancedCurrent { get; set; }
    public decimal? Frequency { get; set; }
    public decimal? Power_Total_Min { get; set; }
    public decimal? Power_Total { get; set; }
    public decimal? Power_Total_Abs { get; set; }
    public decimal? Power_Total_Max { get; set; }
    public decimal? ApparentPower_Total_Min { get; set; }
    public decimal? ApparentPower_Total { get; set; }
    public decimal? ApparentPower_Total_Max { get; set; }
    public decimal? PF_Total { get; set; }
    public decimal? PF_Total_Abs { get; set; }
    public decimal? CosPhi { get; set; }
    public decimal? TanPhi { get; set; }
    public decimal? QV_Total { get; set; }
    public decimal? Voltage_Phase1 { get; set; }
    public decimal? Voltage_Phase2 { get; set; }
    public decimal? Voltage_Phase3 { get; set; }
    public decimal? Voltage_Phase1_Max { get; set; }
    public decimal? Voltage_Phase2_Max { get; set; }
    public decimal? Voltage_Phase3_Max { get; set; }
    public decimal? Voltage_Phase1_Min { get; set; }
    public decimal? Voltage_Phase2_Min { get; set; }
    public decimal? Voltage_Phase3_Min { get; set; }
    public decimal? ShortTermPercebility_Phase1 { get; set; }
    public decimal? ShortTermPercebility_Phase2 { get; set; }
    public decimal? ShortTermPercebility_Phase3 { get; set; }
    public decimal? THD_PhaseToPhase12 { get; set; }
    public decimal? THD_PhaseToPhase23 { get; set; }
    public decimal? THD_PhaseToPhase31 { get; set; }
    public decimal? HarmonicVoltage1_Phase1 { get; set; }
    public decimal? HarmonicVoltage1_Phase2 { get; set; }
    public decimal? HarmonicVoltage1_Phase3 { get; set; }
    public decimal? HarmonicVoltage2_Phase1 { get; set; }
    public decimal? HarmonicVoltage2_Phase2 { get; set; }
    public decimal? HarmonicVoltage2_Phase3 { get; set; }
    public decimal? HarmonicVoltage3_Phase1 { get; set; }
    public decimal? HarmonicVoltage3_Phase2 { get; set; }
    public decimal? HarmonicVoltage3_Phase3 { get; set; }
    public decimal? HarmonicVoltage4_Phase1 { get; set; }
    public decimal? HarmonicVoltage4_Phase2 { get; set; }
    public decimal? HarmonicVoltage4_Phase3 { get; set; }
    public decimal? HarmonicVoltage5_Phase1 { get; set; }
    public decimal? HarmonicVoltage5_Phase2 { get; set; }
    public decimal? HarmonicVoltage5_Phase3 { get; set; }
    public decimal? HarmonicVoltage6_Phase1 { get; set; }
    public decimal? HarmonicVoltage6_Phase2 { get; set; }
    public decimal? HarmonicVoltage6_Phase3 { get; set; }
    public decimal? HarmonicVoltage7_Phase1 { get; set; }
    public decimal? HarmonicVoltage7_Phase2 { get; set; }
    public decimal? HarmonicVoltage7_Phase3 { get; set; }
    public decimal? HarmonicVoltage8_Phase1 { get; set; }
    public decimal? HarmonicVoltage8_Phase2 { get; set; }
    public decimal? HarmonicVoltage8_Phase3 { get; set; }
    public decimal? HarmonicVoltage9_Phase1 { get; set; }
    public decimal? HarmonicVoltage9_Phase2 { get; set; }
    public decimal? HarmonicVoltage9_Phase3 { get; set; }
    public decimal? HarmonicVoltage10_Phase1 { get; set; }
    public decimal? HarmonicVoltage10_Phase2 { get; set; }
    public decimal? HarmonicVoltage10_Phase3 { get; set; }
    public decimal? HarmonicVoltage11_Phase1 { get; set; }
    public decimal? HarmonicVoltage11_Phase2 { get; set; }
    public decimal? HarmonicVoltage11_Phase3 { get; set; }
    public decimal? HarmonicVoltage12_Phase1 { get; set; }
    public decimal? HarmonicVoltage12_Phase2 { get; set; }
    public decimal? HarmonicVoltage12_Phase3 { get; set; }
    public decimal? HarmonicVoltage13_Phase1 { get; set; }
    public decimal? HarmonicVoltage13_Phase2 { get; set; }
    public decimal? HarmonicVoltage13_Phase3 { get; set; }
    public decimal? HarmonicVoltage14_Phase1 { get; set; }
    public decimal? HarmonicVoltage14_Phase2 { get; set; }
    public decimal? HarmonicVoltage14_Phase3 { get; set; }
    public decimal? HarmonicVoltage15_Phase1 { get; set; }
    public decimal? HarmonicVoltage15_Phase2 { get; set; }
    public decimal? HarmonicVoltage15_Phase3 { get; set; }
    public decimal? HarmonicVoltage16_Phase1 { get; set; }
    public decimal? HarmonicVoltage16_Phase2 { get; set; }
    public decimal? HarmonicVoltage16_Phase3 { get; set; }
    public decimal? HarmonicVoltage17_Phase1 { get; set; }
    public decimal? HarmonicVoltage17_Phase2 { get; set; }
    public decimal? HarmonicVoltage17_Phase3 { get; set; }
    public decimal? HarmonicVoltage18_Phase1 { get; set; }
    public decimal? HarmonicVoltage18_Phase2 { get; set; }
    public decimal? HarmonicVoltage18_Phase3 { get; set; }
    public decimal? HarmonicVoltage19_Phase1 { get; set; }
    public decimal? HarmonicVoltage19_Phase2 { get; set; }
    public decimal? HarmonicVoltage19_Phase3 { get; set; }
    public decimal? HarmonicVoltage20_Phase1 { get; set; }
    public decimal? HarmonicVoltage20_Phase2 { get; set; }
    public decimal? HarmonicVoltage20_Phase3 { get; set; }
    public decimal? HarmonicVoltage21_Phase1 { get; set; }
    public decimal? HarmonicVoltage21_Phase2 { get; set; }
    public decimal? HarmonicVoltage21_Phase3 { get; set; }
    public decimal? HarmonicVoltage22_Phase1 { get; set; }
    public decimal? HarmonicVoltage22_Phase2 { get; set; }
    public decimal? HarmonicVoltage22_Phase3 { get; set; }
    public decimal? HarmonicVoltage23_Phase1 { get; set; }
    public decimal? HarmonicVoltage23_Phase2 { get; set; }
    public decimal? HarmonicVoltage23_Phase3 { get; set; }
    public decimal? HarmonicVoltage24_Phase1 { get; set; }
    public decimal? HarmonicVoltage24_Phase2 { get; set; }
    public decimal? HarmonicVoltage24_Phase3 { get; set; }
    public decimal? HarmonicVoltage25_Phase1 { get; set; }
    public decimal? HarmonicVoltage25_Phase2 { get; set; }
    public decimal? HarmonicVoltage25_Phase3 { get; set; }
    public decimal? HarmonicVoltage26_Phase1 { get; set; }
    public decimal? HarmonicVoltage26_Phase2 { get; set; }
    public decimal? HarmonicVoltage26_Phase3 { get; set; }
    public decimal? HarmonicVoltage27_Phase1 { get; set; }
    public decimal? HarmonicVoltage27_Phase2 { get; set; }
    public decimal? HarmonicVoltage27_Phase3 { get; set; }
    public decimal? HarmonicVoltage28_Phase1 { get; set; }
    public decimal? HarmonicVoltage28_Phase2 { get; set; }
    public decimal? HarmonicVoltage28_Phase3 { get; set; }
    public decimal? HarmonicVoltage29_Phase1 { get; set; }
    public decimal? HarmonicVoltage29_Phase2 { get; set; }
    public decimal? HarmonicVoltage29_Phase3 { get; set; }
    public decimal? HarmonicVoltage30_Phase1 { get; set; }
    public decimal? HarmonicVoltage30_Phase2 { get; set; }
    public decimal? HarmonicVoltage30_Phase3 { get; set; }
    public decimal? HarmonicVoltage31_Phase1 { get; set; }
    public decimal? HarmonicVoltage31_Phase2 { get; set; }
    public decimal? HarmonicVoltage31_Phase3 { get; set; }
    public decimal? HarmonicVoltage32_Phase1 { get; set; }
    public decimal? HarmonicVoltage32_Phase2 { get; set; }
    public decimal? HarmonicVoltage32_Phase3 { get; set; }
    public decimal? HarmonicVoltage33_Phase1 { get; set; }
    public decimal? HarmonicVoltage33_Phase2 { get; set; }
    public decimal? HarmonicVoltage33_Phase3 { get; set; }
    public decimal? HarmonicVoltage34_Phase1 { get; set; }
    public decimal? HarmonicVoltage34_Phase2 { get; set; }
    public decimal? HarmonicVoltage34_Phase3 { get; set; }
    public decimal? HarmonicVoltage35_Phase1 { get; set; }
    public decimal? HarmonicVoltage35_Phase2 { get; set; }
    public decimal? HarmonicVoltage35_Phase3 { get; set; }
    public decimal? HarmonicVoltage36_Phase1 { get; set; }
    public decimal? HarmonicVoltage36_Phase2 { get; set; }
    public decimal? HarmonicVoltage36_Phase3 { get; set; }
    public decimal? HarmonicVoltage37_Phase1 { get; set; }
    public decimal? HarmonicVoltage37_Phase2 { get; set; }
    public decimal? HarmonicVoltage37_Phase3 { get; set; }
    public decimal? HarmonicVoltage38_Phase1 { get; set; }
    public decimal? HarmonicVoltage38_Phase2 { get; set; }
    public decimal? HarmonicVoltage38_Phase3 { get; set; }
    public decimal? HarmonicVoltage39_Phase1 { get; set; }
    public decimal? HarmonicVoltage39_Phase2 { get; set; }
    public decimal? HarmonicVoltage39_Phase3 { get; set; }
    public decimal? HarmonicVoltage40_Phase1 { get; set; }
    public decimal? HarmonicVoltage40_Phase2 { get; set; }
    public decimal? HarmonicVoltage40_Phase3 { get; set; }
    public decimal? HarmonicVoltage41_Phase1 { get; set; }
    public decimal? HarmonicVoltage41_Phase2 { get; set; }
    public decimal? HarmonicVoltage41_Phase3 { get; set; }
    public decimal? HarmonicVoltage42_Phase1 { get; set; }
    public decimal? HarmonicVoltage42_Phase2 { get; set; }
    public decimal? HarmonicVoltage42_Phase3 { get; set; }
    public decimal? HarmonicVoltage43_Phase1 { get; set; }
    public decimal? HarmonicVoltage43_Phase2 { get; set; }
    public decimal? HarmonicVoltage43_Phase3 { get; set; }
    public decimal? HarmonicVoltage44_Phase1 { get; set; }
    public decimal? HarmonicVoltage44_Phase2 { get; set; }
    public decimal? HarmonicVoltage44_Phase3 { get; set; }
    public decimal? HarmonicVoltage45_Phase1 { get; set; }
    public decimal? HarmonicVoltage45_Phase2 { get; set; }
    public decimal? HarmonicVoltage45_Phase3 { get; set; }
    public decimal? HarmonicVoltage46_Phase1 { get; set; }
    public decimal? HarmonicVoltage46_Phase2 { get; set; }
    public decimal? HarmonicVoltage46_Phase3 { get; set; }
    public decimal? HarmonicVoltage47_Phase1 { get; set; }
    public decimal? HarmonicVoltage47_Phase2 { get; set; }
    public decimal? HarmonicVoltage47_Phase3 { get; set; }
    public decimal? HarmonicVoltage48_Phase1 { get; set; }
    public decimal? HarmonicVoltage48_Phase2 { get; set; }
    public decimal? HarmonicVoltage48_Phase3 { get; set; }
    public decimal? HarmonicVoltage49_Phase1 { get; set; }
    public decimal? HarmonicVoltage49_Phase2 { get; set; }
    public decimal? HarmonicVoltage49_Phase3 { get; set; }
    public decimal? HarmonicVoltage50_Phase1 { get; set; }
    public decimal? HarmonicVoltage50_Phase2 { get; set; }
    public decimal? HarmonicVoltage50_Phase3 { get; set; }
    public decimal? THD_Phase1 { get; set; }
    public decimal? THD_Phase2 { get; set; }
    public decimal? THD_Phase3 { get; set; }
    public string? OTHERS { get; set; }
}