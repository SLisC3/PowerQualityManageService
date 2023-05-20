

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PowerQualityManageService.Infrastructure.Models;
public class Template 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Content { get; set; } = null!;
    public short Type { get; set; } = 0;
}

public class TemplateDTO
{
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? Content { get; set; } = null!;
    public short? Type { get; set; } = 0;
}
