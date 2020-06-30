namespace NiceOne.Profiles
{
    using AutoMapper;

    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Categories;
    using NiceOne.DTOs.Cities;
    using NiceOne.DTOs.Countries;
    using NiceOne.DTOs.Places;
    using NiceOne.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<PlaceSetModel, Place>();
            CreateMap<PlaceGetModel, PlaceSetModel>();

            CreateMap<Category, CategoryGetModel>();
            CreateMap<CategorySetModel, Category>();
            CreateMap<CategoryGetModel, CategorySetModel>();

            CreateMap<Country, CountryModel>();
            CreateMap<CountryModel, Country>();

            CreateMap<City, CityModel>();
            CreateMap<CityModel, City>();
        }
    }
}
