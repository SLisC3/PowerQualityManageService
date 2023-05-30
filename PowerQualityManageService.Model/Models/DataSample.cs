namespace PowerQualityManageService.Infrastructure.Models;

public class DataSample
{
    public string MeasuringPoint { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool Flagging { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}