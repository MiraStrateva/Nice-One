namespace NiceOne.Services.Countries
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Countries;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly IMapper mapper;
        public CountryService(NiceOneDbContext data, IMapper mapper)
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
