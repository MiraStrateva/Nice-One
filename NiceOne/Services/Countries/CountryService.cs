namespace NiceOne.Services.Countries
{
    using NiceOne.Data;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Countries;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(NiceOneDbContext data) 
            : base(data)
        {
        }

        public async Task<IEnumerable<CountryGetModel>> GetAsync()
        {
            var result = await this.GetAllAsync();
            return result.Select(c => new CountryGetModel()
            {
                Id = c.Id,
                Name = c.Name
            });
        }
    }
}
