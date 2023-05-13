using Microsoft.AspNetCore.Http;
using PowerQualityManageService.Core.Helpers;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Utils.Enums;
using PowerQualityManageService.Core.Utils.Extensions;
using System.Data;
using System.IO;
using System.Text;

namespace PowerQualityManageService.Core.Repositories.Concrete;

public class DataManagementRepository : IDataManagementRepository
{
    public async Task<string?> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string? filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return fileName;
    }
    public Stream? Download(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
        try
        {
            return File.OpenRead(filePath);
        }
        catch
        {
            return null;
        }
    }
    public async Task<DataTable> ReadRows(Stream stream, List<ColumnHeader> headers, int count)
    {
        DataTable dt = new DataTable();
        int timeIdx = headers.FindIndex(x => x.Kind == Kind.Time);
        int dateIdx = headers.FindIndex(x => x.Kind == Kind.Date);
        headers.ForEach(x => dt.Columns.Add(x.Name, x.VariableType));
        if (timeIdx >= 0 && dateIdx >= 0) dt.Columns.RemoveAt(timeIdx);

        if (stream.Position != 0) { stream.Position = 0; }
        using (StreamReader sr = new StreamReader(stream))
        {
            if (sr.EndOfStream) return dt;
            await sr.ReadLineAsync();
            for (int i = 0; i < count; i++)
            {
                if (sr.EndOfStream) return dt;
                string? row = await sr.ReadLineAsync();
                if (row == null) return dt; ;
                List<string> rows = row.Split(';').ToList();
                if (timeIdx >= 0 && dateIdx >= 0)
                {
                    rows[dateIdx] = string.Concat(rows[dateIdx], " ", rows[timeIdx]);
                    rows.RemoveAt(timeIdx);
                }
                DataRow dr = dt.NewRow();
                for (int j = 0; j < rows.Count; j++)
                {
                    dr[j] = !string.IsNullOrWhiteSpace(rows[j]) ? rows[j] : dt.Columns[j].DataType.GetDefaultValue().ToString();
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    public async Task<DataTable> ReadRowsNoDispose(Stream stream, List<ColumnHeader> headers, int count)
    {
        DataTable dt = new DataTable();
        int timeIdx = headers.FindIndex(x => x.Kind == Kind.Time);
        int dateIdx = headers.FindIndex(x => x.Kind == Kind.Date);
        headers.ForEach(x => dt.Columns.Add(x.Name, x.VariableType));
        if (timeIdx >= 0 && dateIdx >= 0) dt.Columns.RemoveAt(timeIdx);
        long position = stream.Position;
        bool headersFromCache = stream.Position ==0;

        using (NoDisposeInputStreamReader sr = new NoDisposeInputStreamReader(stream, Encoding.UTF8))
        {
            if (sr.EndOfStream) return dt;
            for (int i = 0; i < count; i++)
            {
                string? row = await sr.ReadLineAsync(); 
                if (string.IsNullOrEmpty(row)) return dt;
                if (headersFromCache)
                {
                    headersFromCache= false;
                    continue;
                }
                List<string> rows = row.Split(';').ToList();

                if (timeIdx >= 0 && dateIdx >= 0)
                {
                    if (timeIdx >= 0 && dateIdx >= 0) rows[dateIdx] = string.Concat(rows[dateIdx], " ", rows[timeIdx]);
                    rows.RemoveAt(timeIdx);
                }
                DataRow dr = dt.NewRow();
                for (int j = 0; j < rows.Count; j++)
                {
                    dr[j] = !string.IsNullOrWhiteSpace(rows[j]) ? rows[j] : dt.Columns[j].DataType.GetDefaultValue().ToString();
                }
                dt.Rows.Add(dr);
                if (sr.EndOfStream) return dt;
                position += row.Length + 2;
                if (i == count - 1)
                {
                    stream.Position = position;
                }
                
            }
            
        }
        return dt;
    }
    public async Task<List<ColumnHeader>> GetHeaders(Stream stream)
    {
        List<string>? headers = null;
        using (NoDisposeInputStreamReader sr = new NoDisposeInputStreamReader(stream, Encoding.UTF8))
        {
            var headerstring = await sr.ReadLineAsync();
            if (headerstring != null)
            {
                headers = headerstring.Split(new char[] { ',', ';' }).ToList();
            }
            if (headerstring != null) stream.Position -= 1024 - (Encoding.UTF8.GetByteCount(headerstring) + 2);
        }
        if (headers == null) return new List<ColumnHeader>();
        IEnumerable<string> trimmedHeaders = ColumnHeaderRegexHelper.TrimQuotes(headers);
        List<ColumnHeader> columns = new List<ColumnHeader>();
        foreach (var h in trimmedHeaders)
        {
            columns.Add(new ColumnHeader(h));
        }
        return columns;
    }

    public void Delete(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);
        File.Delete(filePath);
    }
}