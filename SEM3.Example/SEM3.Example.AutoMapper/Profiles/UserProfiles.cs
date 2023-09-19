using AutoMapper;
using SEM3.Example.AutoMapper.Models;
using SEM3.Example.AutoMapper.ViewModels;

namespace SEM3.Example.AutoMapper.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.ProductCode, opt => opt.MapFrom(src => src.Id));
        }
    }
}
