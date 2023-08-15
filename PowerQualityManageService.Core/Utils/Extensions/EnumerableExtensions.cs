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
    public static double[] ToDoubleArray(this IEnumerable<object?> items)
    {
        return items.Cast<decimal>().Select(x => Decimal.ToDouble(x)).ToArray();
    }

    public static List<int> IndexesOf (this IEnumerable<object?> items, Func<object?, bool> condition) 
    {
        List<int> result = new List<int>();
        for (int i = 0; i < items.Count(); i++)
        {
            if(condition(items.ElementAt(i)))
                result.Add(i);
        }
        return result;
    }

    public static IEnumerable<T> ExceptByIndexes<T> (this IEnumerable<T> items, IEnumerable<int> indexes)
    {
        return items.Where((item, index) => !indexes.Contains(index));
    }
}
