using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NiceOne.DTOs.Categories;
using NiceOne.DTOs.Cities;
using NiceOne.DTOs.Countries;
using NiceOne.DTOs.Places;
using NiceOne.Services.Categories;
using NiceOne.Services.Cities;
using NiceOne.Services.Countries;
using NiceOne.Services.Places;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NiceOne.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly ICategoryService categoryService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        public PlaceController(IPlaceService placeService, 
            ICategoryService categoryService, 
            ICountryService countryService,
            ICityService cityService)
        {
            this.placeService = placeService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.cityService = cityService;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetAsync());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            List<CountryGetModel> countryList = new List<CountryGetModel>(await countryService.GetAsync());
            countryList.Insert(0, new CountryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PlaceSetModel placeSetModel)
        {
            if (this.ModelState.IsValid)
            {
                await this.placeService.CreateAsync(placeSetModel);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return this.View(placeSetModel);
        }

        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var places = this.placeService.GetByCategory(categoryId).Result;
            return this.View("List", places);
        }

        public async Task<JsonResult> GetCities(int countryId)
        {
            var cityList = new List<CityGetModel>(await cityService.GetCitiesByCountryAsync(countryId));
            cityList.Insert(0, new CityGetModel { Id = 0, Name = "Select" });

            return Json(new SelectList(cityList, "Id", "Name"));
        }
    }
}
