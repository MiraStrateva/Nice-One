namespace NiceOne.Place.Profiles
{
    using AutoMapper;

    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.DTOs.Categories;
    using NiceOne.Place.DTOs.Feedbacks;
    using NiceOne.Place.DTOs.Places;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlaceSetModel, Place>();
            CreateMap<PlaceGetModel, PlaceSetModel>();

            CreateMap<Category, CategoryGetModel>();
            CreateMap<CategorySetModel, Category>();
            CreateMap<CategoryGetModel, CategorySetModel>();

            CreateMap<Feedback, FeedbackGetModel>()
                .ForMember(f => f.Place, opt => opt.MapFrom(x => x.Place.Name));
            CreateMap<FeedbackSetModel, Feedback>();
            CreateMap<FeedbackGetModel, FeedbackSetModel>();
        }
    }
}
