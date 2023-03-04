using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Extensions;
public static class EnumerableExtensions
{
    public static IEnumerable<T> SetValues<T>( this IEnumerable<T> items, Action<T> update)
    {
        foreach (var item in items)
        {
            update(item);
        }
        return items;
    }
}
