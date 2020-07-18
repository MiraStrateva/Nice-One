namespace NiceOne.Place.Services.Feedbacks
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Place.Data;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Feedbacks;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FeedbackService : BaseService<NiceOnePlaceDbContext, Feedback>, IFeedbackService
    {
        private readonly IMapper mapper;
        public FeedbackService(NiceOnePlaceDbContext data, IMapper mapper)
            : base(data)
            => this.mapper = mapper;

        public async Task DeleteAsync(int id)
        {
            var feedback = new Feedback { Id = id };
            await this.DeleteAsync(feedback);
        }

        public async Task<FeedbackGetModel> GetByIdAsync(int feedbackId)
            => await this.mapper
                .ProjectTo<FeedbackGetModel>(this.Data.Feedbacks)
                .FirstOrDefaultAsync(c => c.Id == feedbackId);

        public async Task<IEnumerable<FeedbackGetModel>> GetByPlaceAsync(int placeId)
            => await this.mapper
                .ProjectTo<FeedbackGetModel>(this.Data.Feedbacks)
                .Where(f => f.PlaceId == placeId)
                .ToListAsync();

        public async Task<IEnumerable<FeedbackGetModel>> GetByUserAsync(string userId)
            => await this.mapper
                .ProjectTo<FeedbackGetModel>(this.Data.Feedbacks)
                .Where(f => f.UserId == userId)
                .ToListAsync();
    }
}
