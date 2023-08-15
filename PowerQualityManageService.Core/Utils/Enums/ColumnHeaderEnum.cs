using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Enums;

public enum Kind : byte
{
    Unrecognized = 0,
    Date = 1,
    Time = 2, 
    Flagging =  3,
    Frequency = 4,
    CosPhi = 5,
    TanPhi = 6,
    UnbalancedVoltage = 7,
    UnbalancedCurrent = 8,
    Voltage = 10,
    Current = 11,
    Power = 12,
    ShortTermPercebility = 13,
    LongTermPercebility = 14,
    ApparentPower = 15,
    QV = 16,
    PF = 17,
    THD = 18,
    HarmonicVoltage = 19
}
public enum  TypeOfMeasurement : byte
{
    NotApplicable = 0,
    Phase = 1,
    PhaseToPhase = 4,
    Neutral = 8,
    Total = 9,
}

public enum TypeOfValue: byte
{
    NotApplicable = 0,
    Mean = 1,
    Min = 2,
    Max = 3,
    Abs = 4
}

