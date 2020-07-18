namespace NiceOne.Client.Controllers
{
    using AutoMapper;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Client.Models.Location;
    using NiceOne.Client.Services.Location;
    using NiceOne.Infrastructure;

    public class CountryController : BaseController
    {
        private readonly IMapper mapper;
        private readonly ILocationService locationService;

        public CountryController(ILocationService service, IMapper mapper)
        {
            locationService = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
            => View("List", await locationService.GetCountries());

        [HttpGet]
        public async Task<IActionResult> Cities(int id)
        {
            ViewBag.CountryId = id;
            return View("Cities", await locationService.GetCities(id));
        }

        [AuthorizeAdministrator]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeAdministrator]
        public async Task<IActionResult> Create(CountryModel input)
            => await this.Handle(
                async () => await this.locationService.CreateCountry(input),
                success: RedirectToAction(nameof(CountryController.All)),
                failure: View(input));

        [AuthorizeAdministrator]
        public async Task<IActionResult> Edit(int Id)
        {
            var country = await locationService.GetCountry(Id);

            return View(country);
        }

        [HttpPost]
        [AuthorizeAdministrator]
        public async Task<IActionResult> Edit(int id, CountryModel input)
            => await this.Handle(
                async () => await this.locationService.EditCountry(id, input),
                success: RedirectToAction(nameof(CountryController.All)),
                failure: View(input));
        
        [AuthorizeAdministrator]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [AuthorizeAdministrator]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.locationService.DeleteCountry(Id);
            return RedirectToAction(nameof(CountryController.All));
        }

        [AuthorizeAdministrator]
        public async Task<IActionResult> CreateCity(int countryId)
        {
            List<CountryModel> countryList = new List<CountryModel>(await locationService.GetCountries());
            countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            return View(new CityModel { CountryId = countryId });
        }

        [HttpPost]
        [AuthorizeAdministrator]
        public async Task<IActionResult> CreateCity(CityModel input)
            => await this.Handle(
                async () => await this.locationService.CreateCity(input),
                success: RedirectToAction(nameof(CountryController.Cities), new { id = input.CountryId }),
                failure: View(input));

        [AuthorizeAdministrator]
        public async Task<IActionResult> EditCity(int Id)
        {
            var city = await locationService.GetCity(Id);

            return View(city);
        }

        [HttpPost]
        [AuthorizeAdministrator]
        public async Task<IActionResult> EditCity(CityModel input)
        {
            var city = await locationService.GetCity(input.Id);
            return await this.Handle(
                async () => await this.locationService.EditCity(input.Id, input),
                success: RedirectToAction(nameof(CountryController.Cities), new { id = city.CountryId }),
                failure: View(input));
        }

        [AuthorizeAdministrator]
        public IActionResult DeleteCity(int id)
        {
            return this.View(id);
        }

        [AuthorizeAdministrator]
        public async Task<IActionResult> ConfirmCityDelete(int Id)
        {
            var city = await locationService.GetCity(Id);
            await this.locationService.DeleteCity(Id);
            return RedirectToAction(nameof(CountryController.Cities), new { id = city.CountryId });
        }
    }
}
