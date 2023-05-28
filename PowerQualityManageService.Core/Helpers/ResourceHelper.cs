using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

namespace PowerQualityManageService.Core.Helpers;
public class ResourceHelper
{
    private static readonly Lazy<ResourceHelper> _instance = new Lazy<ResourceHelper>(() => new ResourceHelper());
    private readonly ResourceManager _resourceManager;

    public static ResourceHelper Instance => _instance.Value;

    private ResourceHelper()
    {
        _resourceManager = new ResourceManager("PowerQualityManageService.Resources.Report",
            AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "PowerQualityManageService")!);
    }

    public string GetString(string key)
    {
        return _resourceManager.GetString(key) ?? string.Empty;
    }
}