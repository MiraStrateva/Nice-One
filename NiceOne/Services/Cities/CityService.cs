namespace NiceOne.Services.Cities
{
    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Cities;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CityService : BaseService<City>, ICityService
    {
        public CityService(NiceOneDbContext data) 
            : base(data)
        {
        }

        public async Task<IEnumerable<CityGetModel>> GetCitiesByCountryAsync(int countryId)
        {
            var result = await this.GetAllAsync(c => c.CountryId == countryId, c => c.Name);
            return result.Select(c => new CityGetModel { Id = c.Id, Name = c.Name });
        }
    }
}
