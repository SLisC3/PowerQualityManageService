using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Enums;
public enum ColumnHeader
{
    Date = 0,
    Time = 1,
    Flagging = 2,
    Frequency = 11,
    UnbalancedVoltage =  12,
    UnbalancedCurrent = 13,
    CosPhi = 14,
    SinPhi = 15,
    Harmonic = 16,
    QVTotal = 50,
    U12 = 101,
    U12Min = 102,
    U12Max = 103,
    U23 = 104,
    U23Min = 105,
    U23Max = 106,
    U31 = 107,
    U31Min = 108,
    U31Max = 109,
    U1 = 110,
    U1Min = 111,
    U1Max = 112,
    U2 = 113,
    U2Min = 114, 
    U2Max = 115,
    U3 =  116,
    U3Min = 117,
    U3Max = 118,
    INeutral = 151,
    INeutralMin = 152,
    INeutralMax = 153,
    I1 = 154,
    I1Min = 155,
    I1Max = 156,
    I2 = 157,
    I2Min = 158,
    I2Max =  159,
    I3 = 160,
    I3Min = 161,
    I3Max = 162,
    Pst12 = 201,
    Pst23 = 202,
    Pst31 = 203,
    Pst1 = 204,
    Pst2 = 205,
    Pst3 = 206,
    Plt12 = 221,
    Plt23 = 222,
    Plt31 = 223,
    PTotal = 251,
    PTotalMin = 252,
    PTotalMax = 253,
    PTotalAbs = 254,
    STotal = 255,
    STotalMin = 256,
    STotalMax = 257,
    PFTotal = 258,
    PFTotalAbs = 259
}

public enum Value : short
{
    Date = 0,
    Time = 1,
    Flagging =  2,
    Voltage = 3,
    Current = 4,
    Power = 5,
    ShortTermPercebility = 6,
    LongTermPercebility = 7,
    HarmonicVoltage = 8,
    Frequency = 9,
    CosPhi = 10,
    TanPhi = 11,
    ReactivePower = 12,
    ApparentPower = 13,
    UnbalancedVoltage = 14,
    UnbalancedCurrent = 15,
    PF = 16,
    QV = 17

}
public enum  PointOfMeasurement : short
{
    NotApplicable = 0,
    L1 = 1,
    L2 = 2,
    L3 = 3,
    L12 = 4,
    L23 = 5,
    L31 = 6,
    Neutral = 7,
    Total = 8,

}

public enum TypeOfValue: short
{
    NotApplicable = 0,
    Mean = 1,
    Min = 2,
    Max = 3,
    Abs = 4
}
public enum TypeOfMeasurement : short
{
    NotApplicable = 0,
    Phase = 1,
    PhaseToPhase = 2
}
