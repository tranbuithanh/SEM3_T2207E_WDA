using AutoMapper;
using Domain;
using Service.DTOs;

namespace Service.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfileDTO, UserProfile>().ReverseMap();
        }
    }
}
