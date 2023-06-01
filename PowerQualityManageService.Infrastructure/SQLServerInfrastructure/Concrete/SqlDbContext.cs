using Microsoft.EntityFrameworkCore;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
public class SqlDbContext : DbContext
{
	public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
	{

	}

	public DbSet<Template> Templates { get; set; }
}
