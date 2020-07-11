namespace NiceOne.Location.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Location.Data.Entities;
    using NiceOne.Location.DTOs.Cities;
    using NiceOne.Location.DTOs.Countries;
    using NiceOne.Location.Services.Cities;
    using NiceOne.Location.Services.Countries;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        [Authorize]
        [Route(nameof(All))]
        public async Task<IActionResult> All()
        {
            var countries = await countryService.GetAsync();
            return Ok(countries);
            // return View("List", countries);
        }

        [Authorize] 
        [Route(nameof(Cities))]
        public async Task<IActionResult> Cities(int countryId)
        {
            ViewBag.CountryId = countryId;
            var cities = await cityService.GetCitiesByCountryAsync(countryId);
            return View("Cities", cities);
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(CountryModel countryModel)
        {
            if (this.ModelState.IsValid)
            {
                var country = mapper.Map<Country>(countryModel);
                await this.countryService.CreateAsync(country);
                return RedirectToAction(nameof(CountryController.All));
            }

            return View(countryModel);
        }

        [Authorize]
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(int Id)
        {
            var country = await countryService.GetByIdAsync(Id);

            return View(mapper.Map<CountryModel>(country));
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(CountryModel countryModel)
        {
            if (this.ModelState.IsValid)
            {
                var country = await this.countryService.FindAsync(countryModel.Id);

                country.Name = countryModel.Name;

                await this.countryService.SaveAsync(country);
                return RedirectToAction(nameof(CountryController.All));
            }

            return View(countryModel);
        }

        [Authorize]
        [Route(nameof(Delete))]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        [Route(nameof(ConfirmDelete))]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.countryService.DeleteAsync(Id);
            return RedirectToAction(nameof(CountryController.All));
        }


        [Authorize]
        [Route(nameof(CreateCity))]
        public async Task<IActionResult> CreateCity(int countryId)
        {
            List<CountryModel> countryList = new List<CountryModel>(await countryService.GetAsync());
            countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            return View(new CityModel { CountryId = countryId });
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(CreateCity))]
        public async Task<IActionResult> CreateCity(CityModel cityModel)
        {
            if (this.ModelState.IsValid)
            {
                var city = mapper.Map<City>(cityModel);
                await this.cityService.CreateAsync(city);
                return RedirectToAction(nameof(CountryController.Cities), new { countryId = city.CountryId });
            }

            return View(cityModel);
        }

        [Authorize]
        [Route(nameof(EditCity))]
        public async Task<IActionResult> EditCity(int Id)
        {
            var city = await cityService.GetByIdAsync(Id);

            return View(mapper.Map<CityModel>(city));
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(EditCity))]
        public async Task<IActionResult> EditCity(CityModel cityModel)
        {
            if (this.ModelState.IsValid)
            {
                var city = await cityService.FindAsync(cityModel.Id);

                city.Name = cityModel.Name;

                await cityService.SaveAsync(city);
                return RedirectToAction(nameof(CountryController.Cities), new { countryId = city.CountryId });
            }

            return View(cityModel);
        }

        [Authorize]
        [Route(nameof(DeleteCity))]
        public IActionResult DeleteCity(int id)
        {
            return this.View(id);
        }

        [Authorize]
        [Route(nameof(ConfirmCityDelete))]
        public async Task<IActionResult> ConfirmCityDelete(int Id)
        {
            var city = await cityService.FindAsync(Id);
            await this.cityService.DeleteAsync(Id);
            return RedirectToAction(nameof(CountryController.Cities), new { countryId = city.CountryId });
        }
    }
}
