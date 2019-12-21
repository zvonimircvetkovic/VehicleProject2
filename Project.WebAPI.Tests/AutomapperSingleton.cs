using AutoMapper;
using Project.Service;
using Project.WebAPI.Models;

namespace Project.WebAPI.Tests
{
    public class AutomapperSingleton
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new ServiceMapperProfile());
                        cfg.AddProfile(new APIMapperProfile());
                    });
                    IMapper mapper = config.CreateMapper();
                    _mapper = mapper;
                }
                return _mapper;
            }
        }
    }
}
