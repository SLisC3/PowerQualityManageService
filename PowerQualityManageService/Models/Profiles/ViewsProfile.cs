using AutoMapper;
using PowerQualityManageService.Model.Models;

namespace PowerQualityManageService.Models.Profiles;

public class ViewsProfile : Profile
{
    public ViewsProfile() 
    {
        CreateMap<TemplateModel, TemplateEditModel>().ReverseMap();
        CreateMap<TemplateEditModel, Template>().ReverseMap();
    }
}
