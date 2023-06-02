using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Infrastructure.MongoDBInfrastructure.Concrete;
public class MongoDbConfig
{
    public string? ConnectionString { get; set; }
    public string? Database { get; set; }
    public string? DataSamples { get; set; }
    public string? Templates { get; set; }
}
