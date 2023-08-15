using System.Transactions;

namespace PowerQualityManageService.Model.Models;

public enum NormCalculationMethod : ushort
{
    GreaterThan = 0,
    LesserThan = 1
}

public class CalculationResult
{
    public bool Result { get; set; }
    public string? ErrorMessage { get; set; }
    public decimal? PercentageResult { get; set; }
    public static CalculationResult Passed(decimal percentageResult)
    {
        return new CalculationResult() { Result = true, PercentageResult = percentageResult };
    }
    public static CalculationResult Failed(string message, decimal? percentageResult)
    {
        return new CalculationResult() { Result = false, ErrorMessage = message, PercentageResult = percentageResult };
    }
}

public abstract class INormCalculationMethod
{
    public abstract CalculationResult Calculate(IEnumerable<decimal?> samples, string? key = null);
    public CalculationResult Compare(IEnumerable<decimal> samples, decimal compareValue, NormCalculationMethod method, decimal percentageOfTotalPeriod = 100)
    {
        switch (method)
        {
            case NormCalculationMethod.GreaterThan:
                decimal correctCount1 = (decimal)samples.Where(x => x >= compareValue).Count();
                decimal allCount1 = (decimal)samples.Count();
                decimal percentageGreatherThan = correctCount1 / allCount1 * 100;
                if (percentageGreatherThan >= percentageOfTotalPeriod) return CalculationResult.Passed(percentageGreatherThan);
                else return CalculationResult.Failed("Zbyt dużo próbek poniżej wartości normy", percentageGreatherThan);
            case NormCalculationMethod.LesserThan:
                decimal correctCount2 = (decimal)samples.Where(x => x <= compareValue).Count();
                decimal allCount2 = (decimal)samples.Count();
                decimal percentageLesserThan = correctCount2 / allCount2 * 100;
                if (percentageLesserThan >= percentageOfTotalPeriod) return CalculationResult.Passed(percentageLesserThan);
                else return CalculationResult.Failed("Zbyt dużo próbek powyżej wartości normy", percentageLesserThan);
            default:
                return CalculationResult.Failed("Błędna metoda", null);
        }
    }

    public bool TryFilterSamples(IEnumerable<decimal?> notFilteredSamples, out IEnumerable<decimal> samples)
    {
        int corruptedData = notFilteredSamples.Where(x => x == null).Count();
        
        if(corruptedData > 10) { samples = new List<decimal>(); return false; }

        try
        {
            samples = notFilteredSamples.Where(x => x != null).Select(x => (decimal)x!);
            return true;
        }
        catch (Exception)
        {
            samples = new List<decimal>();
            return false;
        }
        
    }
}
public class NotSufficientSamples : INormCalculationMethod
{
    public override CalculationResult Calculate(IEnumerable<decimal?> samples, string? key = null)
    {
        return CalculationResult.Failed("Brak wymaganych danych", null);
    }
}

public class Harmonic : INormCalculationMethod
{
    public override CalculationResult Calculate(IEnumerable<decimal?> samplesNotFiltered, string? key = null)
    {
        IEnumerable<decimal> samples;
        if(!TryFilterSamples(samplesNotFiltered, out samples)) return CalculationResult.Failed("Niewystarczająca ilość danych", null);
        if(string.IsNullOrEmpty(key)) return CalculationResult.Failed("Nieoczekiwany Błąd", null);
        string harmonic = key.Split('_').First();
        switch (harmonic)
        {
            case "HarmonicVoltage2":
                return Compare(samples, 2m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage4":
                return Compare(samples, 1m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage6":
            case "HarmonicVoltage8":
            case "HarmonicVoltage10":
            case "HarmonicVoltage12":
            case "HarmonicVoltage14": 
            case "HarmonicVoltage16":
            case "HarmonicVoltage18":
            case "HarmonicVoltage20":
            case "HarmonicVoltage22":
            case "HarmonicVoltage24":
                return Compare(samples, 0.5m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage3":
                return Compare(samples, 5, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage9":
                return Compare(samples, 1.5m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage15":
            case "HarmonicVoltage21":
                return Compare(samples, 0.5m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage5":
                return Compare(samples, 6m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage7":
                return Compare(samples, 5m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage11":
                return Compare(samples, 3.5m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage13":
                return Compare(samples, 3m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage17":
                return Compare(samples, 2m, NormCalculationMethod.LesserThan);
            case "HarmonicVoltage19":
            case "HarmonicVoltage23":
            case "HarmonicVoltage25":
                return Compare(samples, 1.5m, NormCalculationMethod.LesserThan);
            default:
                return CalculationResult.Failed("Nieobsłużony klucz", null);
        }
    }
}
public class InRangePercentageComparison : INormCalculationMethod
{
    private readonly decimal _normValue;
    private readonly decimal _rangeOver;
    private readonly decimal _rangeUnder;
    private readonly decimal _percentageOfTotalPeriod;

    public InRangePercentageComparison(decimal normValue, decimal rangeOver, decimal rangeUnder, decimal percentageOfTotalPeriod = 100)
    {
        _normValue = normValue;
        _rangeOver = rangeOver;
        _rangeUnder = rangeUnder;
        _percentageOfTotalPeriod = percentageOfTotalPeriod;
    }
    public InRangePercentageComparison(decimal normValue, decimal range, decimal percentageOfTotalPeriod = 100)
    {
        _normValue = normValue;
        _rangeOver = range;
        _rangeUnder = range;
        _percentageOfTotalPeriod = percentageOfTotalPeriod;
    }
    public override CalculationResult Calculate(IEnumerable<decimal?> samplesNotFiltered, string? key = null)
    {
        IEnumerable<decimal> samples;
        if (!TryFilterSamples(samplesNotFiltered, out samples)) return CalculationResult.Failed("Niewystarczająca ilość danych", null);
        CalculationResult firstResult = Compare(samples, _normValue - _normValue*_rangeUnder/100, NormCalculationMethod.GreaterThan, _percentageOfTotalPeriod);
        if (firstResult.Result == false) { return firstResult; }
        CalculationResult secondResult = Compare(samples, _normValue + _normValue*_rangeOver/100, NormCalculationMethod.LesserThan, _percentageOfTotalPeriod);
        if (secondResult.Result == false) { return secondResult; }
        int samplesCount = samples.Count();
        decimal percentageResult = ( (samplesCount * (decimal)firstResult.PercentageResult!/100 + samplesCount * (decimal)secondResult.PercentageResult!/100!)) / (2* samplesCount) * 100;
        if (percentageResult >= _percentageOfTotalPeriod) return CalculationResult.Passed(percentageResult);
        return CalculationResult.Failed("Zbyt dużo próbek poza zakresem", percentageResult);
    }
}

public class InRangeComparison : INormCalculationMethod
{
    private readonly decimal _normValue;
    private readonly decimal _rangeOver;
    private readonly decimal _rangeUnder;
    private readonly decimal _percentageOfTotalPeriod;

    public InRangeComparison(decimal normValue, decimal rangeOver, decimal rangeUnder, decimal percentageOfTotalPeriod = 100)
    {
        _normValue = normValue;
        _rangeOver = rangeOver;
        _rangeUnder = rangeUnder;
        _percentageOfTotalPeriod = percentageOfTotalPeriod;
    }
    public InRangeComparison(decimal normValue, decimal range, decimal percentageOfTotalPeriod = 100)
    {
        _normValue = normValue;
        _rangeOver = range;
        _rangeUnder = -1 * range;
        _percentageOfTotalPeriod = percentageOfTotalPeriod;
    }
    public override CalculationResult Calculate(IEnumerable<decimal?> samplesNotFiltered, string? key = null)
    {
        IEnumerable<decimal> samples;
        if (!TryFilterSamples(samplesNotFiltered, out samples)) return CalculationResult.Failed("Niewystarczająca ilość danych", null);
        CalculationResult firstResult = Compare(samples, _rangeUnder, NormCalculationMethod.GreaterThan, _percentageOfTotalPeriod);
        if (firstResult.Result == false) { return firstResult; }
        CalculationResult secondResult = Compare(samples, _rangeOver, NormCalculationMethod.LesserThan, _percentageOfTotalPeriod);
        if (secondResult.Result == false) { return secondResult; }
        int samplesCount = samples.Count();
        decimal percentageResult = (2 * (decimal)samplesCount - (samplesCount * (decimal)firstResult.PercentageResult! + samplesCount * (decimal)secondResult.PercentageResult!)) / (samplesCount) * 100;
        if (percentageResult >= _percentageOfTotalPeriod) return CalculationResult.Failed("Zbyt dużo próbek poza zakresem", percentageResult);
        return CalculationResult.Passed(percentageResult);
    }
}
public class SimpleComparison : INormCalculationMethod
{
    private readonly decimal _compareValue;
    private readonly NormCalculationMethod _method;
    private readonly decimal _percentageOfTotalPeriod;

    public SimpleComparison(decimal compareValue, NormCalculationMethod method, decimal percentageOfTotalPeriod = 100)
    {
        _compareValue = compareValue;
        _method = method;
        _percentageOfTotalPeriod = percentageOfTotalPeriod;
    }
    public override CalculationResult Calculate(IEnumerable<decimal?> samplesNotFiltered, string? key = null)
    {
        IEnumerable<decimal> samples;
        if (!TryFilterSamples(samplesNotFiltered, out samples)) return CalculationResult.Failed("Niewystarczająca ilość danych", null);
        return Compare(samples, _compareValue, _method, _percentageOfTotalPeriod);
    }
}
