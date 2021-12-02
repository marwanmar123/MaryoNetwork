using AutoMapper;
using MaryoNetwork.Models.Messenger;
using MaryoNetwork.ViewModels;

namespace MaryoNetwork.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomViewModel>();
            CreateMap<RoomViewModel, Room>();
        }
    }
}
