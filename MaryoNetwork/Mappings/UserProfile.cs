using AutoMapper;
using MaryoNetwork.Models;
using MaryoNetwork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
