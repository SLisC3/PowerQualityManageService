using Microsoft.EntityFrameworkCore;
using PowerQualityManageService.Core.PDFGenerator;
using PowerQualityManageService.Core.PDFGenerator.PageModels;
using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Repositories.Concrete;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Services.Concrete;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
using QuestPDF.Previewer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services/repos.
builder.Services.AddScoped<IDataManagementDbRepository, DataManagementMongoDbRepository>();
builder.Services.AddScoped<IDataAcquisitionRepository, DataAcquisitionRepository>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IDataAcquisitionService, DataAcquisitionService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();

builder.Services.AddRazorPages();

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


var p = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Imgs\\harmoniczne3-5.jpg"));

//var filePath = "test.pdf";
var model = new ReportModel();
model.StationName = "testowa";
model.FromDate= DateTime.Now.AddDays(-1);
model.ToDate= DateTime.Now.AddDays(1);
model.Results = new List<SingleResult>() { new SingleResult() { Name ="1a", Success= true }, new SingleResult() { Name = "1b", Success = false, Message = "B³ad" }, new SingleResult() { Name = "2"} };
model.ResultCharts = new List<ChartData>() {
    new ChartData() { 
        Name = "Charcik", Data = new Dictionary<string, double[]>() {
            { "V1", new double[] {1,7,-2 } },
            { "V2", new double[] {3,4,5 } },
            { "V3", new double[] {0,5,10 } },
            
        }, DateLabels = new List<DateTime>() {DateTime.Now.AddDays(-1), DateTime.Now, DateTime.Now.AddDays(1)} 
    }
};
var document = new ReportDocument(model);
document.ShowInPreviewer();
    //GeneratePdf(filePath);

//Process.Start("explorer.exe", filePath);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();
app.Run();

