using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.Helpers;
public class NoDisposeInputStreamReader : StreamReader
{
    public NoDisposeInputStreamReader(Stream stream)
    : base(stream)
    {
        
    }
    public NoDisposeInputStreamReader(Stream stream, Encoding encoding)
    : base(stream,encoding)
    {

    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(false);
    }
}