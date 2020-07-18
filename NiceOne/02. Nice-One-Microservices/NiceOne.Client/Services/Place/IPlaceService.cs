namespace NiceOne.Client.Services.Place
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NiceOne.Client.Models.Place.Categories;
    using NiceOne.Client.Models.Place.Feedbacks;
    using NiceOne.Client.Models.Place.Places;
    using Refit;

    public interface IPlaceService
    {
        [Get("/Place/All")]
        Task<IEnumerable<PlaceListGetModel>> All();

        [Get("/Place/ByCategory/{id}")]
        Task<IEnumerable<PlaceListGetModel>> ByCategory(int id);

        [Get("/Place/MyPlaces")]
        Task<IEnumerable<PlaceListGetModel>> MyPlaces();

        [Post("/Place/SearchPlaces/{search}")]
        Task<IEnumerable<PlaceListGetModel>> SearchPlaces(string search);

        [Post("/Place/Create")]
        Task Create([Body] PlaceSetModel place);

        [Post("/Place/Edit/{id}")]
        Task Edit(int id, [Body] PlaceSetModel place);

        [Get("/Place/ConfirmDelete/{id}")]
        Task Delete(int id);

        [Post("/Place/AddFeedback")]
        Task AddFeedback([Body] FeedbackSetModel feedback);

        [Get("/Feedback/AllByUser")]
        Task<IEnumerable<FeedbackGetModel>> GetFeedbacksForCurrentUser();

        [Get("/Feedback/Details/{id}")]
        Task<FeedbackGetModel> GetFeedback(int id);

        [Post("/Feedback/Edit/{id}")]
        Task EditFeedback(int id, [Body] FeedbackSetModel feedback);

        [Get("/Feedback/ConfirmDelete/{id}")]
        Task DeleteFeedback(int id);
    }
}
