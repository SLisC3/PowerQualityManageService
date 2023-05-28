

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityManageService.Infrastructure.Models;
public class Template 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Chart> Charts { get; set; } = null!;
}

public class Chart
{
    public string Name { get; set; } = null!;
    public Dictionary<string, string> Signals { get; set; } = null!;
}

public class TemplateDTO
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public Dictionary<string, Chart> Charts { get; set; } = null!;
}
