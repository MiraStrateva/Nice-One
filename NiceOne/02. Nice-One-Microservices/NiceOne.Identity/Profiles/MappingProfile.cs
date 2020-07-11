namespace NiceOne.Identity.Profiles
{
    using AutoMapper;

    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
