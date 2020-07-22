namespace NiceOne.Location.Services.Countries
{
    using AutoMapper;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.Models.Countries;
    using NiceOne.Services;
    using NiceOne.Messages.Location;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CountryService : BaseService<NiceOneLocationDbContext, Country>, ICountryService
    {
        private readonly IMapper mapper;
        private readonly IBus publisher;
        public CountryService(NiceOneLocationDbContext data, IMapper mapper, IBus publisher)
            : base(data)
        { 
            this.mapper = mapper;
            this.publisher = publisher;
        }

        public async Task<IEnumerable<CountryModel>> GetAsync()
             => await this.mapper
                .ProjectTo<CountryModel>(this.Data.Countries)
                .ToListAsync();

        public async Task<CountryModel> GetByIdAsync(int countryId)
            => await this.mapper
                .ProjectTo<CountryModel>(this.Data.Countries)
                .FirstOrDefaultAsync(c => c.Id == countryId);

        public async Task DeleteAsync(int id)
        {
            var country = new Country { Id = id };
            await this.DeleteAsync(country);
        }

        public override async Task SaveAsync(Country entity)
        {
            await base.SaveAsync(entity);

            await this.publisher.Publish(new CountryUpdatedMessage 
            {
                CountryId = entity.Id,
                CountryName = entity.Name
            });
        }
    }
}
