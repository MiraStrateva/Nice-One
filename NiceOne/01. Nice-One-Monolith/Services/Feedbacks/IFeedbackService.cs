namespace NiceOne.Services.Feedbacks
{
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Feedbacks;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFeedbackService : IBaseService<Feedback>
    {
        Task<IEnumerable<FeedbackGetModel>> GetByPlaceAsync(int placeId);
        Task<FeedbackGetModel> GetByIdAsync(int feedbackId);
        Task DeleteAsync(int id);
        Task<IEnumerable<FeedbackGetModel>> GetByUserAsync(string userId);
    }
}
