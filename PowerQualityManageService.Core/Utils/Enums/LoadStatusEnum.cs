using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Utils.Enums;
public enum SaveStatus : byte
{
    Awaiting = 0,
    InProgress = 1,
    Finished = 3,
    ErrorWhileLoading = 4
}
