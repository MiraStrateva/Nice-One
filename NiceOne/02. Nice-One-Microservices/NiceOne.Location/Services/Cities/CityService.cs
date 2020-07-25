namespace NiceOne.Location.Services.Cities
{
    using AutoMapper;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data.Models;
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.Models.Cities;
    using NiceOne.Messages.Location;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Threading.Tasks;

    public class CityService : BaseService<NiceOneLocationDbContext, City>, ICityService
    {
        private readonly IMapper mapper;
        private readonly IBus publisher;
        public CityService(NiceOneLocationDbContext data, IMapper mapper, IBus publisher)
            : base(data)
        {
            this.Data = data;
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task DeleteAsync(int id)
        {
            var city = new City { Id = id };
            await this.DeleteAsync(city);
        }

        public async Task<CityModel> GetByIdAsync(int cityId)
            => await this.mapper
                .ProjectTo<CityModel>(this.Data.Cities)
                .FirstOrDefaultAsync(c => c.Id == cityId);

        public async Task<IEnumerable<CityModel>> GetCitiesByCountryAsync(int countryId)
            => await this.mapper
                .ProjectTo<CityModel>(this.Data.Cities)
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public override async Task SaveAsync(City entity, Message[] messages)
        {
            var messageData = new CityUpdatedMessage
            {
                CityId = entity.Id,
                CityName = entity.Name
            };

            var message = new Message(messageData);

            await base.SaveAsync(entity, message);

            await this.publisher.Publish(messageData);

            await this.MarkMessageAsPublished(message.Id);
        }
    }
}
