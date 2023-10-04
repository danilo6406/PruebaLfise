using AutoMapper;
using EasySales.Shared;

namespace EasySales.Server.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Reservaciones, Reservaciones>();
        }
    }
}
