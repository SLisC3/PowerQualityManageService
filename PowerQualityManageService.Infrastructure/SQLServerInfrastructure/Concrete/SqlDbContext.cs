using Microsoft.EntityFrameworkCore;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
public class SqlDbContext : DbContext
{
	public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
	{

	}

	public DbSet<TemplateSQL> Templates { get; set; }
    public DbSet<ReportSQL> Reports { get; set; }
    public DbSet<DataSamplesSQL> DataSamples_Single { get; set; }
    public DbSet<MeasuringPointSQL> MeasuringPoints { get; set; }
    public DbSet<DataSamplesSQL_Header> DataSamples_Header { get; set; }
}
