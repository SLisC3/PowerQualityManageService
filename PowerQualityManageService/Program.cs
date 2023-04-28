using PowerQualityManageService.Core.Repositories.Abstract;
using PowerQualityManageService.Core.Repositories.Concrete;
using PowerQualityManageService.Core.Services.Abstract;
using PowerQualityManageService.Core.Services.Concrete;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Abstract;
using PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
using PowerQualityManageService.Infrastructure.SQLServerInfrastructure.Concrete;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDataMongoDbRepository, DataMongoDbRepository>();

builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<SqlConfig>(builder.Configuration.GetSection(nameof(SqlConfig)));

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o=>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

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
