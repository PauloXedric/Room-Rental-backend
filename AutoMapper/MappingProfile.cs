using AutoMapper;
using RRMS.Entities;
using RRMS.Models.ChatMessageModels;
using RRMS.Models.EmergencyContactModels;
using RRMS.Models.Identity;
using RRMS.Models.RoomModels;
using RRMS.Models.UserAccountModels;

namespace RRMS.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {
            //ChatMessage
            CreateMap<ChatMessageEntity, ReadMessageModel>();
            CreateMap<AddMessageModel, ChatMessageEntity>();


            //EmergencyContact
            CreateMap<EmergencyContactEntity, ReadEmergencyContactModel>();
            CreateMap<CreateEmergencyContactModel, EmergencyContactEntity>();
            CreateMap<PatchEmergencyContactModel, EmergencyContactEntity>()
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateRoomModel, RoomEntity>();


            //Room
            CreateMap<RoomEntity, ReadRoomModel>();
            CreateMap<PatchRoomInfoModel, RoomEntity>()
             .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PatchRoomPricingModel, RoomEntity>()
             .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PatchRoomAvailabilityModel, RoomEntity>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


            //UserAccount
            CreateMap<ApplicationUser, ReadUserIdModel>();
            CreateMap<RegisterUserModel, ApplicationUser>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
              .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }


    }
}
