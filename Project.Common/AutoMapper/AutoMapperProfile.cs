using AutoMapper;
using Project.DAL.Entities;
using Project.Model;
using Project.Model.Common;

namespace Project.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MakeEntity, Make>().ReverseMap();
            CreateMap<MakeEntity, IMake>().ReverseMap();
            CreateMap<IMake, Make>().ReverseMap();
            CreateMap<Model.Model, ModelEntity>();
            CreateMap<ModelEntity, Model.Model>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<IModel, ModelEntity>();
            CreateMap<ModelEntity, IModel>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<Model.Model, IModel>();
            CreateMap<IModel, Model.Model>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
        }
    }
}
