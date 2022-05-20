using AutoMapper;
using ConGEST.CongestDbContext;
using ConGEST.DTOs.Holliday;

namespace ConGEST.Profiles
{
    public class HollidayProfile : Profile
    {
        public HollidayProfile()
        {
            CreateMap<Holliday, HollidayDto>();
        }
    }
}
