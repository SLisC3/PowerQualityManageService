using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Services.Concrete;
public class DataService : IDataService
{
    private readonly IDataRepository _dataRepository;

    public DataService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public bool LoadData(Stream stream)
    {
        var dr = ParseData(stream);
        throw new NotImplementedException();
    }

    private DataTable ParseData(Stream stream)
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
