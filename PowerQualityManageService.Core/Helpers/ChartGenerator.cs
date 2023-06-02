using PowerQualityManageService.Model.Models;
using System.Diagnostics;

namespace PowerQualityManageService.Core.Helpers;
public class ChartGenerator
{
    public static byte[] GenerateChart(ChartData data)
    {
#if DEBUG
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
#endif
        // Config
        var plt = new ScottPlot.Plot(1466, 1000);
        var colors = new Queue<System.Drawing.Color>();
        colors.Enqueue(System.Drawing.Color.RoyalBlue);
        colors.Enqueue(System.Drawing.Color.Firebrick);
        colors.Enqueue(System.Drawing.Color.DarkOliveGreen);

#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Configu Chartu " + data.Name + " " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif

        // Prepare dates
        double[] dates = data.DateLabels.Select(x => x.ToOADate()).ToArray();

#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas Preparu dat Chartu " + data.Name + " " + stopwatch.Elapsed);
        stopwatch.Restart();
        stopwatch.Start();
#endif
        // Add Signals
        foreach (KeyValuePair<string, double[]> kvp in data.Data)
        {
            var sig = plt.AddScatter(dates, kvp.Value, lineWidth: 5, color: colors.Dequeue(), label: kvp.Key);
        }
        plt.XAxis.DateTimeFormat(true);

        // Styling
        var legend = plt.Legend();
        legend.FontSize = 24;
        legend.FontBold = true;
        plt.XAxis.TickLabelStyle(rotation: 45, fontSize: 20, fontBold: true);
        plt.YAxis.TickLabelStyle(fontSize: 26, fontBold: true);
        plt.Title(data.Name,true, size: 34);

#if DEBUG
        stopwatch.Stop();
        Console.WriteLine("Czas dodawania signalow dla Chartu " + data.Name + " " + stopwatch.Elapsed);
#endif
        return plt.GetImageBytes();
    }
}
