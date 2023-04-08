using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Concrete;
public class DataRepository : IDataRepository
{
    public bool InsertData(DataTable dt)
    {
        throw new NotImplementedException();
    }

    public DataTable ParseData(Stream stream)
    {
        var headers = CSVHelper.ReadHeaders(stream);
        var trimmedHeaders = ColumnHeaderRegexHelper.TrimQuotes(headers);
        List<ColumnHeader> columns = new List<ColumnHeader>();
        foreach (var h in trimmedHeaders)
        {
            columns.Add(new ColumnHeader(h));
        }
        var dt = CSVHelper.ReadRowsCount(stream, headers, 5);
        return dt;
    }
}
