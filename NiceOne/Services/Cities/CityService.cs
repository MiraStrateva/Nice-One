namespace NiceOne.Services.Cities
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Cities;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CityService : BaseService<City>, ICityService
    {
        private readonly IMapper mapper;
        public CityService(NiceOneDbContext data, IMapper mapper)
            : base(data)
            => this.mapper = mapper;

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
    }
}
