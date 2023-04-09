using PowerQualityManageService.Core.Helpers;
using System.Data;
using System.Reflection.PortableExecutable;

namespace PowerQualityManageService;

public static class CSVHelper
{
    public static DataTable ReadRowsCount(Stream stream, List<string> headers, int rowsCount)
    {
        DataTable dt = new DataTable();
        headers.ForEach(x => dt.Columns.Add(x));

        if(stream.Position != 0) { stream.Position = 0; }
        using (NoDisposeInputStreamReader sr = new NoDisposeInputStreamReader(stream))
        {
            if (sr.EndOfStream) return dt;
            sr.ReadLine();
            for (int i = 0; i < rowsCount; i++)
            {
                if (sr.EndOfStream) return dt;
                var row = sr.ReadLine();
                if (row == null) return dt; ;
                string[] rows = row.Split(';');
                DataRow dr = dt.NewRow();
                for (int j = 0; j < rows.Length; j++)
                {
                    dr[j] = rows[j];
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    public static DataTable ConvertCSVtoDataTable(Stream stream)
    {
        DataTable dt = new DataTable();
        using (NoDisposeInputStreamReader sr = new NoDisposeInputStreamReader(stream))
        {
            var header = sr.ReadLine();
            if (header == null) return dt; ;
            string[] headers = header.Split(';');
            foreach (string h in headers)
            {
                dt.Columns.Add(h);
            }
            while (!sr.EndOfStream)
            {
                var row = sr.ReadLine();
                if (row == null) return dt; ;
                string[] rows = row.Split(';');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    public static List<string> ReadHeaders(Stream stream)
    {
        using (NoDisposeInputStreamReader sr = new NoDisposeInputStreamReader(stream))
        {
            var headerstring = sr.ReadLine();
            if (headerstring != null) { return headerstring.Split(new char[] { ',', ';' }).ToList(); }
        }
        return new List<string>();
    }
}