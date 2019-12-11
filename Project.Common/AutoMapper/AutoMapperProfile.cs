using AutoMapper;
using Project.Common.Models;
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
            CreateMap<IMake, IMakeEntity>().ReverseMap();
            CreateMap<Model.Model, ModelEntity>();
            CreateMap<ModelEntity, Model.Model>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<IModel, ModelEntity>();
            CreateMap<ModelEntity, IModel>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<Model.Model, IModel>();
            CreateMap<IModel, Model.Model>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<IModel, IModelEntity>();
            CreateMap<IModelEntity, IModel>().ForMember(d => d.Id, o => o.Condition(s => s.Id != 0));
            CreateMap<ViewMake, IMake>();
            CreateMap<ViewModel, IModel>();
        }
    }
}
