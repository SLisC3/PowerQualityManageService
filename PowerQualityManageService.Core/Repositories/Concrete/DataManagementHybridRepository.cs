using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers.ExplicitMappings;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
using PowerQualityManageService.Model.Models;
using System.Data;
using System.Diagnostics;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class DataManagementHybridRepository : IDataManagementDbRepository
{
    private readonly SqlDbContext _sqlContext;
    private readonly IMongoCollection<DataSampleId> _dataSamplesIdHybrid;
    public DataManagementHybridRepository(SqlDbContext sqlContext, IMongoDbContext mongoDbContext)
    {
        _sqlContext = sqlContext ?? throw new ArgumentNullException(nameof(sqlContext));
        _dataSamplesIdHybrid = mongoDbContext.DataSamplesIdHybrid;
    }
    public async Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate, string measuringPoint)
    {
        var measuringPointId = await GetMeasuringPointId(measuringPoint);
        var headers = _sqlContext.DataSamples_Header.Where(x => !x.Flagging && x.MeasuringPoints_Id == measuringPointId && x.Date >= startDate && x.Date <= endDate).Select(x => ObjectId.Parse(x.Data_Id));
        var filterDef = new FilterDefinitionBuilder<DataSampleId>();
        var filter = filterDef.In(x => x.Id, headers);

        var result = await _dataSamplesIdHybrid.FindAsync(filter);
        if (result == null) { return null; }
        return result.ToEnumerable();
    }

    public async Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate, string measuringPoint)
    {

//-------------------------------------------------------------------------------
#if DEBUG
        var stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        var measuringPointId = await GetMeasuringPointId(measuringPoint);
        var headers = _sqlContext.DataSamples_Header.Where(x => !x.Flagging && x.MeasuringPoints_Id == measuringPointId && x.Date >= startDate && x.Date <= endDate).Select(x => ObjectId.Parse(x.Data_Id));
        var filterDef = new FilterDefinitionBuilder<DataSampleId>();
        var filter = filterDef.In(x => x.Id, headers);

        var samples = await _dataSamplesIdHybrid.FindAsync(filter);

        var groupedSamples = samples.ToEnumerable().GroupBy(x => x.Date).Select(g => new { Date = g.Key, Data = g.SelectMany(x => x.Data!).Distinct().ToDictionary(kv => kv.Key, kv => kv.Value) }).ToList();


//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[HYBRID] Czas wyciągania Sampli:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        if (groupedSamples.Count == 0) { return null; }
        Dictionary<string, object> columns = groupedSamples.First().Data;

        DataTable dt = new DataTable();

        dt.Columns.Add("Date");
        foreach (KeyValuePair<string, object> kv in columns)
        {
            dt.Columns.Add(kv.Key);
        }

        foreach (var item in groupedSamples)
        {
            item.Data.Add("Date", item.Date);
            DataRow row = dt.NewRow();
            foreach (var entry in item.Data)
            {
                row[entry.Key] = entry.Value;
            }
            dt.Rows.Add(row);
        }

//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[HYBRID] Czas Tworzenia DataTable:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        return dt;
    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return await _sqlContext.MeasuringPoints.Select(x => x.Name).ToListAsync();
    }

    public async Task<(DateTime, DateTime)> GetStartEndDate()
    {
        if (_sqlContext.DataSamples_Header.Count() > 0)
        {
            var min = await _sqlContext.DataSamples_Header.MinAsync(x => x.Date);
            var max = await _sqlContext.DataSamples_Header.MaxAsync(x => x.Date);
            return (min, max);
        }
        return (DateTime.Now, DateTime.Now);
    }

    public async Task<int> InsertDataFromDataTable(DataTable dt, string measuringPoint)
    { 
       if (dt == null)
        {
            return 0;
        }

//-------------------------------------------------------------------------------
#if DEBUG
        var stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------
        var samplesMongo = new List<DataSampleId>();

        foreach (DataRow dr in dt.Rows)
        {
            var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            DateTime date = (DateTime)dictionary["Date"];
            dictionary.Remove("Date");
            bool flagging = !string.IsNullOrEmpty((string?)dictionary["Flagging"]);
            dictionary.Remove("Flagging");

            var sample = new DataSampleId {
                Id = ObjectId.GenerateNewId(),
                MeasuringPoint= measuringPoint,
                Date = date, 
                Flagging = flagging,
                Data = dictionary };

            samplesMongo.Add(sample);
        }
        var measuringPointId = await GetMeasuringPointId(measuringPoint);
        var samplesSql = samplesMongo.MapListDataSampleToHeader(measuringPointId);



//-------------------------------------------------------------------------------
#if DEBUG
stopwatch.Stop();
        Console.WriteLine("[HYBRID] Czas Tworzenia Sampli:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        await _sqlContext.DataSamples_Header.AddRangeAsync(samplesSql);
        await _sqlContext.SaveChangesAsync();
        await _dataSamplesIdHybrid.InsertManyAsync(samplesMongo.AsEnumerable());

        //-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[HYBRID] Czas Zapisu w bazie:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        return samplesSql.Count();
    }

    private async Task<int> GetMeasuringPointId(string measuringPoint)
    {
        var point = await _sqlContext.MeasuringPoints.Where(x => x.Name.Equals(measuringPoint)).FirstOrDefaultAsync();
        if (point == null)
        {
            var newMeasuringPoint = await _sqlContext.AddAsync(new MeasuringPointSQL() { Name = measuringPoint });
            await _sqlContext.SaveChangesAsync();
            return newMeasuringPoint.CurrentValues.GetValue<int>("MeasuringPoint_Id");
        }
        return point.MeasuringPoint_Id;
    }
}
