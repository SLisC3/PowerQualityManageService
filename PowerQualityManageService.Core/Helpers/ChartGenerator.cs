using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Core.Helpers;
public class ChartGenerator
{
    public static byte[] GenerateChart(ChartData data)
    {
        // Config
        var plt = new ScottPlot.Plot(1600, 1200);
        var colors = new Queue<System.Drawing.Color>();
        colors.Enqueue(System.Drawing.Color.RoyalBlue);
        colors.Enqueue(System.Drawing.Color.Firebrick);
        colors.Enqueue(System.Drawing.Color.DarkOliveGreen);

        // Prepare dates
        double[] dates = new double[data.DateLabels.Count()];
        DateTime firstDay = data.DateLabels.First();
        for (int i = 0; i < data.DateLabels.Count(); i++)
            dates[i] = firstDay.AddDays(i).ToOADate();

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

        return plt.GetImageBytes();
    }
}
