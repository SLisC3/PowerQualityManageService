using Azure;
using PowerQualityManageService.Core.PDFGenerator.Abstract;
using PowerQualityManageService.Core.Utils.Enums;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.PDFGenerator;

public static class PagesGenerator
{
    public static IDocumentContainer InsertPage(this IDocumentContainer container, IBasePage basePage)
    {
        return basePage.Compose(container);
    }
}



