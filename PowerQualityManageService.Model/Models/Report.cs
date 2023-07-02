using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PowerQualityManageService.Model.Models;

public class Report
{
    [BsonId]
    public string FileName { get; set; }
    public string Name { get; set; } = null!;
    public string Template { get; set; } = null!;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string MeasuringPoint { get; set; } = null!;
}