namespace NiceOne.Location.Services.Countries
{
    using NiceOne.Location.Data;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.DTOs.Countries;
    using NiceOne.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountryService : IBaseService<NiceOneLocationDbContext, Country>
    {
        Task<IEnumerable<CountryModel>> GetAsync();
        Task<CountryModel> GetByIdAsync(int countryId);
        Task DeleteAsync(int id);
    }
}
