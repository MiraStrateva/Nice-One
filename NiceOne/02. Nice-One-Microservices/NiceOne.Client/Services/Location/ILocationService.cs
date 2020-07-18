namespace NiceOne.Client.Services.Location
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NiceOne.Client.Models.Location;
    
    using Refit;
    
    public interface ILocationService
    {
        [Get("/Country/All")]
        Task<IEnumerable<CountryModel>> GetCountries();

        [Get("/Country/GetById/{id}")]
        Task<CountryModel> GetCountry(int id);

        [Post("/Country/Create")]
        Task CreateCountry([Body] CountryModel input);

        [Put("/Country/{id}")]
        Task EditCountry(int id, [Body] CountryModel input);

        [Get("/Country/ConfirmDelete/{id}")]
        Task DeleteCountry(int id);

        [Get("/Country/Cities/{id}")]
        Task<IEnumerable<CityModel>> GetCities(int id);
        
        [Get("/Country/GetCityById/{id}")]
        Task<CityModel> GetCity(int id);

        [Post("/Country/CreateCity")]
        Task CreateCity([Body] CityModel input);

        [Post("/Country/EditCity/{id}")]
        Task EditCity(int id, [Body] CityModel input);

        [Post("/Country/ConfirmCityDelete/{id}")]
        Task DeleteCity(int id);
    }
}
