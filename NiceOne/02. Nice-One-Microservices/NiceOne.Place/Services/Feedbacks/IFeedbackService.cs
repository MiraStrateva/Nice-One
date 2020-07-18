namespace NiceOne.Place.Services.Feedbacks
{
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Feedbacks;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedbackService : IBaseService<NiceOnePlaceDbContext, Feedback>
    {
        Task<IEnumerable<FeedbackGetModel>> GetByPlaceAsync(int placeId);
        Task<FeedbackGetModel> GetByIdAsync(int feedbackId);
        Task DeleteAsync(int id);
        Task<IEnumerable<FeedbackGetModel>> GetByUserAsync(string userId);
    }
}
