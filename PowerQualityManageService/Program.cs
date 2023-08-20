using Microsoft.EntityFrameworkCore;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Repositories.Concrete;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Services.Concrete;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
using QuestPDF.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

// Add services/repos.
//builder.Services.AddScoped<IDataManagementDbRepository, DataManagementHybridRepository>();
builder.Services.AddScoped<IDataManagementDbRepository, DataManagementSQLRepository>();
//builder.Services.AddScoped<IDataManagementDbRepository, DataManagementMongoDbRepository>();
//builder.Services.AddScoped<IDataManagementDbRepository, DataManagementMongoDbWithIdsRepository>();

builder.Services.AddScoped<IDataAcquisitionRepository, DataAcquisitionRepository>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ILocalFilesRepository, LocalFilesRepository>();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IDataAcquisitionService, DataAcquisitionService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddMvc();
builder.Services.AddMemoryCache();

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

var sqlConfig = builder.Configuration.GetSection(nameof(SqlConfig)).Get<SqlConfig>();
builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(sqlConfig.ConnectionString));

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o=>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});
QuestPDF.Settings.License = LicenseType.Community;
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
app.Run();

