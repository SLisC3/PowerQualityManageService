namespace PowerQualityManageService.Infrastructure.Models;

public class DataSample
{
    public DateTime Date { get; set; }
    public bool Flagging { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}