using Microsoft.Build.Framework;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Models;

public class TemplateModel
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; } 
    public List<Dictionary<string,string>> Charts { get; set; } = new List<Dictionary<string, string>>();
}

public class TemplateEditModel
{
    public string PreviousName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Dictionary<string, string>> Charts { get; set; } = new List<Dictionary<string, string>>();
}
