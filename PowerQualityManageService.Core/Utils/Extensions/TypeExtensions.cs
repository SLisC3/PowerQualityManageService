using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Extensions;
public static class TypeExtensions
{
    public static object GetDefaultValue(this Type t)
    {
        if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            return Activator.CreateInstance(t);
        else if (t == typeof(string))
            return string.Empty;
        else
            return null;
    }
}
