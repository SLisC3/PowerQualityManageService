using System.Data;

namespace PowerQualityManageService;

public static class CSVHelper
{
    public static DataTable ConvertCSVtoDataTable(Stream stream)
    {
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(stream))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
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
        using (StreamReader sr = new StreamReader(stream))
        {
            var headerstring = sr.ReadLine();
            if (headerstring != null) { return headerstring.Split(new char[] { ',', ';' }).ToList(); }        
        }
        return new List<string>();
    }
}