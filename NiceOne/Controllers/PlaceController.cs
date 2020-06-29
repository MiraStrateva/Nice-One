﻿namespace NiceOne.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NiceOne.Data.Entities;
    using NiceOne.DTOs.Categories;
    using NiceOne.DTOs.Cities;
    using NiceOne.DTOs.Countries;
    using NiceOne.DTOs.Places;
    using NiceOne.Services.Categories;
    using NiceOne.Services.Cities;
    using NiceOne.Services.Countries;
    using NiceOne.Services.Places;

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class PlaceController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPlaceService placeService;
        private readonly ICategoryService categoryService;
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        public PlaceController(IMapper mapper,
            IPlaceService placeService, 
            ICategoryService categoryService, 
            ICountryService countryService,
            ICityService cityService)
        {
            this.mapper = mapper;
            this.placeService = placeService;
            this.categoryService = categoryService;
            this.countryService = countryService;
            this.cityService = cityService;
        }

        public async Task<IActionResult> Details(int Id)
        {
            var place = await this.placeService.GetByIdAsync(Id);
            return View("Details", place);
        }

        [Authorize]
        public async Task<IActionResult> Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId; 

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetAsync());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            List<CountryGetModel> countryList = new List<CountryGetModel>(await countryService.GetAsync());
            countryList.Insert(0, new CountryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PlaceSetModel placeSetModel)
        {
            if (this.ModelState.IsValid)
            {
                var place = mapper.Map<Place>(placeSetModel);
                await this.placeService.CreateAsync(place);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(placeSetModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
        {
            var place = await placeService.GetByIdAsync(Id);

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetAsync());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            List<CountryGetModel> countryList = new List<CountryGetModel>(await countryService.GetAsync());
            countryList.Insert(0, new CountryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            List<CityGetModel> cityList = new List<CityGetModel>(await cityService.GetCitiesByCountryAsync(place.CountryId));
            cityList.Insert(0, new CityGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCities = cityList;

            return View(mapper.Map<PlaceSetModel>(place));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(PlaceSetModel placeSetModel)
        {
            if (this.ModelState.IsValid)
            {
                var place = await this.placeService.FindAsync(placeSetModel.Id);

                place.Name = placeSetModel.Name;
                place.Description = placeSetModel.Description;
                place.CategoryId = placeSetModel.CategoryId;
                place.CityId = placeSetModel.CityId;

                await this.placeService.SaveAsync(place);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(placeSetModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.placeService.DeleteAsync(Id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> ByCategory(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = categoryService.GetCategoryName(categoryId);

            var places = await placeService.GetByCategoryAsync(categoryId);
            return View("List", places);
        }

        [Authorize]
        public async Task<IActionResult> MyPlaces()
        {
            var places = this.placeService.GetByUserAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value).Result;
            return View("List", places);
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var places = this.placeService.AllAysnc().Result;
            return View("List", places);
        }

        public async Task<JsonResult> GetCities(int countryId)
        {
            var cityList = new List<CityGetModel>(await cityService.GetCitiesByCountryAsync(countryId));
            cityList.Insert(0, new CityGetModel { Id = 0, Name = "Select" });

            return Json(new SelectList(cityList, "Id", "Name"));
        }
    }
}
