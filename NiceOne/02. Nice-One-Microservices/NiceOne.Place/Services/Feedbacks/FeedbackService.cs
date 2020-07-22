namespace NiceOne.Place.Services.Feedbacks
{
    using AutoMapper;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Messages.Place;
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
        private readonly IBus publisher;

        public FeedbackService(NiceOnePlaceDbContext data, IMapper mapper, IBus publisher)
            : base(data)
        {
            this.mapper = mapper;
            this.publisher = publisher;
        }

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

        public override async Task CreateAsync(Feedback entity)
        {
            await base.CreateAsync(entity);
            await publisher.Publish(new FeedbackCreatedMessage
            {
                Rating = entity.Rating,
                Text = entity.Text
            });
        }
    }
}
