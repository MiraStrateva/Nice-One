namespace NiceOne.Location.Services.Countries
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.DTOs.Countries;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CountryService : BaseService<NiceOneLocationDbContext, Country>, ICountryService
    {
        private readonly IMapper mapper;
        public CountryService(NiceOneLocationDbContext data, IMapper mapper)
            : base(data)
            => this.mapper = mapper;

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
    }
}
