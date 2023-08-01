using Microsoft.IdentityModel.Tokens;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Helpers;

public static class NormResultCalculator
{
    public static IEnumerable<CalculationResult> Calculate (SingleNormDefinition singleNormDefinition)
    {
        return singleNormDefinition.Samples.Select( kvp => singleNormDefinition.CalculationMethod.Calculate(kvp.Value, kvp.Key));
    }
}
