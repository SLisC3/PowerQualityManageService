using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PowerQualityManageService.Model.Models;
public class Template 
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<ChartDataDefinition>? Charts { get; set; } = null!;
}
