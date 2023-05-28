using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerQualityManageService.Core.PDFGenerator.PageModels;
public class PageModelsProfile : Profile
{
	public PageModelsProfile()
	{
		CreateMap<ReportModel, OpeningPageModel>();
		CreateMap<ReportModel, ResultPageModel>();
		CreateMap<ReportModel, CustomChartPageModel>();
	}
}
