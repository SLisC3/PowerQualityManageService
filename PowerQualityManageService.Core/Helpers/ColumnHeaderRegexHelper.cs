using PowerQualityManageService.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public static class ColumnHeaderRegexHelper
{
    public static IEnumerable<string> TrimQuotes(IEnumerable<string> input)
    {
        return input.SetValues(x => x.Trim(new char[] {' ','\'','"'}));
    }
}
