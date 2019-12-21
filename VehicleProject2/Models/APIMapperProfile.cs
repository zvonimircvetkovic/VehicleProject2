using AutoMapper;
using Project.Model.Common;

namespace Project.WebAPI.Models
{
    public class APIMapperProfile : Profile
    {
        public APIMapperProfile()
        {
            CreateMap<ViewMake, IMake>().ReverseMap();
            CreateMap<ViewModel, IModel>().ReverseMap();
        }
    }
}
