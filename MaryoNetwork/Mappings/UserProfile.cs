using AutoMapper;
using MaryoNetwork.Models;
using MaryoNetwork.ViewModels;

namespace MaryoNetwork.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<UserViewModel, User>();
        }
    }
}
