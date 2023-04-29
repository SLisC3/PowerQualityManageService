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
    private readonly IDataMongoDbRepository _dataRepository;

    public DataService(IDataMongoDbRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public DataTable LoadData(Stream stream)
    {
        return ParseData(stream);
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
        var dt = CSVHelper.ReadRowsCount(stream, columns, 5);
        return dt;
    }

    public bool Test(DataTable dt)
    {
        _dataRepository.InsertDataFromDataTable(dt);
        return false;
    }
}
