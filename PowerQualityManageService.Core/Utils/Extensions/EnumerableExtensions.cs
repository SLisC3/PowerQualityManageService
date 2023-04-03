using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Extensions;
public static class EnumerableExtensions
{
    public static IEnumerable<T> SetValues<T>(this IEnumerable<T> items, Func<T, T> update)
    {
        return items.Select(x=> update(x));
    }
}
