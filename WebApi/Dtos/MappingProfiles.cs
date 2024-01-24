using AutoMapper;
using Core.Entities;

namespace WebApi.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>()
                .ForMember(p => p.Role, x => x.MapFrom(a => a.Role.ToString()));
        }
    }
}
