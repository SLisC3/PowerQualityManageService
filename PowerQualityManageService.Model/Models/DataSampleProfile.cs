using AutoMapper;

namespace PowerQualityManageService.Model.Models;
public class DataSampleProfile : Profile
{
    public DataSampleProfile()
    {
        CreateMap<DataSampleId, DataSamplesSQL_Header>()
            .ForMember(dest => dest.Data_Id, opt => opt.MapFrom(src => src.Id));
    }
}

