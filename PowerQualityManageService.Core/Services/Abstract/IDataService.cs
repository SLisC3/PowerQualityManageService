using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Services.Abstract;
public interface IDataService
{
    DataTable LoadData(Stream stream);
    bool Test(DataTable table);
}
