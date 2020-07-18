namespace NiceOne.Client.Profiles
{
    using AutoMapper;
    using NiceOne.Client.Models.Place.Categories;
    using NiceOne.Client.Models.Place.Feedbacks;
    using NiceOne.Client.Models.Place.Places;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlaceGetModel, PlaceSetModel>();

            CreateMap<CategoryGetModel, CategorySetModel>();

            CreateMap<FeedbackGetModel, FeedbackSetModel>();
        }
    }
}
