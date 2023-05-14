using Microsoft.EntityFrameworkCore;
using PowerQualityManageService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
public class SqlDbContext : DbContext
{
	public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
	{

	}

	public DbSet<Template> Templates { get; set; }
}
