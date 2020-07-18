namespace NiceOne.Location.Controllers
{
    using AutoMapper;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using NiceOne.Controllers;
    using NiceOne.Infrastructure;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.Models.Cities;
    using NiceOne.Location.Models.Countries;
    using NiceOne.Location.Services.Cities;
    using NiceOne.Location.Services.Countries;
    using Microsoft.AspNetCore.Authorization;

    public class CountryController : ApiController
    {
        private readonly IMapper mapper;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;

        public CountryController(ICountryService service, ICityService cityService, IMapper mapper)
        {
            countryService = service;
            this.cityService = cityService;
            this.mapper = mapper;
        }
        
        [Route(nameof(All))]
        public async Task<IActionResult> All()
            => Ok(await countryService.GetAsync());

        [Route(nameof(GetById) + PathSeparator + Id)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await countryService.GetByIdAsync(id));

        [HttpPost]
        //[AuthorizeAdministrator]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(CountryModel countryModel)
        {
            if (this.ModelState.IsValid)
            {
                var country = mapper.Map<Country>(countryModel);
                await this.countryService.CreateAsync(country);
                return Ok();
            }

            return BadRequest("Model is not valid.");
        }

        [HttpPut]
        //[Authorize]
        [Route(Id)]
        public async Task<IActionResult> Edit(int id, CountryModel countryModel)
        {
            if (this.ModelState.IsValid)
            {
                var country = await this.countryService.FindAsync(countryModel.Id);

                country.Name = countryModel.Name;

                await this.countryService.SaveAsync(country);
                return Ok();
            }

            return BadRequest("Model is not valid."); 
        }

        //[AuthorizeAdministrator]
        [Route(nameof(ConfirmDelete) + PathSeparator + Id)]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.countryService.DeleteAsync(Id);
            return Ok();
        }

        [HttpGet]
        [Route(nameof(Cities) + PathSeparator + Id)]
        public async Task<IActionResult> Cities(int id)
            => Ok(await cityService.GetCitiesByCountryAsync(id));

        [Route(nameof(GetCityById) + PathSeparator + Id)]
        public async Task<IActionResult> GetCityById(int id)
            => Ok(await cityService.GetByIdAsync(id));

        [HttpPost]
        //[AuthorizeAdministrator]
        [Route(nameof(CreateCity))]
        public async Task<IActionResult> CreateCity(CityModel cityModel)
        {
            if (this.ModelState.IsValid)
            {
                var city = mapper.Map<City>(cityModel);
                await this.cityService.CreateAsync(city);
                return Ok();
            }

            return BadRequest("Model is not valid.");
        }

        [HttpPost]
        //[AuthorizeAdministrator]
        [Route(nameof(EditCity) + PathSeparator + Id)]
        public async Task<IActionResult> EditCity(int id, CityModel cityModel)
        {
            if (this.ModelState.IsValid)
            {
                var city = await cityService.FindAsync(cityModel.Id);

                city.Name = cityModel.Name;

                await cityService.SaveAsync(city);
                return Ok();
            }

            return BadRequest("Model is not valid.");
        }

        //[AuthorizeAdministrator]
        [Route(nameof(ConfirmCityDelete)+PathSeparator+Id)]
        public async Task<IActionResult> ConfirmCityDelete(int Id)
        {
            await this.cityService.DeleteAsync(Id);
            return Ok();
        }
    }
}
