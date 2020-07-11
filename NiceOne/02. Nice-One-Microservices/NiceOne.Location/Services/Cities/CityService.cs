namespace NiceOne.Location.Services.Cities
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.DTOs.Cities;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CityService : BaseService<NiceOneLocationDbContext, City>, ICityService
    {
        private readonly IMapper mapper;
        public CityService(NiceOneLocationDbContext data, IMapper mapper)
            : base(data)
        {
            this.Data = data;
            this.mapper = mapper;
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
    }
}
