using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PowerQualityManageService.Model.Models;

public class DataSample
{
    public string MeasuringPoint { get; set; } = null!;
    public DateTime Date { get; set; }
    public bool Flagging { get; set; }
    public Dictionary<string, object>? Data { get; set; }
}

public class DataSampleId : DataSample
{
    [BsonId]
    public ObjectId Id { get; set; }
}