using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Repositories.Abstract;
public interface IDataRepository
{
    DataTable ParseData(Stream stream);
    bool InsertData(DataTable dt);

}
