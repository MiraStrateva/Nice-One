namespace NiceOne.Location.Profiles
{
    using AutoMapper;

    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.Models.Cities;
    using NiceOne.Location.Models.Countries;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryModel>();
            CreateMap<CountryModel, Country>();

            CreateMap<City, CityModel>();
            CreateMap<CityModel, City>();
        }
    }
}
