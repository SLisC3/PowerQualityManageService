using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class FilenameHelper
{
    public static string ToBaseFileName(this string filepath)
    {
        return Path.GetFileNameWithoutExtension(filepath);
    }

    public static string ToFilePath(this string filename)
    {
        return filename + ".pdf";
    }
}
