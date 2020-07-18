namespace NiceOne.Place.Profiles
{
    using AutoMapper;

    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Categories;
    using NiceOne.Place.Models.Feedbacks;
    using NiceOne.Place.Models.Places;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlaceSetModel, Place>();

            CreateMap<Category, CategoryGetModel>();
            CreateMap<CategorySetModel, Category>();

            CreateMap<Feedback, FeedbackGetModel>()
                .ForMember(f => f.Place, opt => opt.MapFrom(x => x.Place.Name));
            CreateMap<FeedbackSetModel, Feedback>();
        }
    }
}
