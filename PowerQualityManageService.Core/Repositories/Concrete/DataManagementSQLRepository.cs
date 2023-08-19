using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PowerQualityManageService.Core.Helpers.ExplicitMappings;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
using PowerQualityManageService.Model.Models;
using SkiaSharp;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace PowerQualityManageService.Core.Repositories.Concrete;
public class DataManagementSQLRepository : IDataManagementDbRepository
{
    private readonly SqlDbContext _context;
    public DataManagementSQLRepository(SqlDbContext context)
    {
        _context= context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<DataSample>?> GetDataSamples(DateTime startDate, DateTime endDate, string measuringPoint)
    {
        int measuringPointId = await GetMeasuringPointId(measuringPoint);
        var samples = _context.DataSamples_Single.Where(x => !x.Flagging && x.MeasuringPoints_Id == measuringPointId && x.Date >= startDate && x.Date <= endDate);
        return samples.Select(x => x.ToDataSample(measuringPoint)).ToList();
    }

    public async Task<DataTable?> GetDataSamplesDT(DateTime startDate, DateTime endDate, string measuringPoint)
    {

//-------------------------------------------------------------------------------
#if DEBUG
        var stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        try
        {
            int measuringPointId = await GetMeasuringPointId(measuringPoint);
            var samples = await _context.DataSamples_Single
                .Where(x => x.MeasuringPoints_Id == measuringPointId && x.Date >= startDate && x.Date <= endDate)
                .Select(x => x.ToDataSample(measuringPoint))
                .ToListAsync();

            var groupedSamples = samples.GroupBy(x => x.Date).Select(g => new { Date = g.Key, Data = g.SelectMany(x => x.Data!).Distinct().ToDictionary(kv => kv.Key, kv => kv.Value) }).ToList();

//-------------------------------------------------------------------------------
#if DEBUG
            stopwatch.Stop();
            Console.WriteLine("[SQL] Czas wyciągania Sampli:  " + stopwatch.Elapsed);
            stopwatch.Restart();
            stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

            if (groupedSamples.Count == 0) { return null; }
            Dictionary<string, object> columns = groupedSamples.First().Data;

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Date");
            foreach (KeyValuePair<string, object> kv in columns)
            {
                dataTable.Columns.Add(kv.Key);
            }

            foreach (var item in groupedSamples)
            {
                item.Data.Add("Date", item.Date);
                DataRow row = dataTable.NewRow();
                foreach (var entry in item.Data)
                {
                    row[entry.Key] = entry.Value;
                }
                dataTable.Rows.Add(row);
            }

//-------------------------------------------------------------------------------
#if DEBUG
            stopwatch.Stop();
            Console.WriteLine("[SQL] Czas Tworzenia DataTable:  " + stopwatch.Elapsed);
            stopwatch.Restart();
            stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

            return dataTable;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }

    public async Task<List<string>> GetMeasuringPoints()
    {
        return await _context.MeasuringPoints.Select(x => x.Name).ToListAsync();
    }

    public async Task<(DateTime, DateTime)> GetStartEndDate()
    {
        if (_context.DataSamples_Single.Count() > 0)
        {
            var min = await _context.DataSamples_Single.MinAsync(x => x.Date);
            var max = await _context.DataSamples_Single.MaxAsync(x => x.Date);
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

        var measuringPointId = await GetMeasuringPointId(measuringPoint);
        var samples = new List<DataSamplesSQL>();
        foreach (DataRow dr in dt.Rows)
        {
            var dataSample = dr.MapDataRowToObject<DataSamplesSQL>();
            dataSample.MeasuringPoints_Id = measuringPointId;
            samples.Add(dataSample);
        }

//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[SQL] Czas Tworzenia Sampli:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        await _context.DataSamples_Single.AddRangeAsync(samples);
        await _context.SaveChangesAsync();

//-------------------------------------------------------------------------------
#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("[SQL] Czas Zapisu w bazie:  " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
//-------------------------------------------------------------------------------

        return samples.Count();
    }

    private async Task<int> GetMeasuringPointId(string measuringPoint)
    {
        var point = await _context.MeasuringPoints.Where(x=> x.Name.Equals(measuringPoint)).FirstOrDefaultAsync();
        if(point == null)
        {
            var newMeasuringPoint = await _context.AddAsync(new MeasuringPointSQL() { Name = measuringPoint });
            await _context.SaveChangesAsync();
            return newMeasuringPoint.CurrentValues.GetValue<int>("MeasuringPoint_Id");
        }
        return point.MeasuringPoint_Id;
    }
}
