namespace NiceOne.Place.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NiceOne.Controllers;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.DTOs.Categories;
    using NiceOne.Place.DTOs.Feedbacks;
    using NiceOne.Place.DTOs.Places;
    using NiceOne.Place.Services.Categories;
    using NiceOne.Place.Services.Feedbacks;
    using NiceOne.Place.Services.Places;
    using NiceOne.Services.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IPlaceService placeService;
        private readonly ICategoryService categoryService;
        private readonly IFeedbackService feedbackService;
        private readonly ICurrentUserService currentUserService;

        public PlaceController(IMapper mapper,
            IPlaceService placeService, 
            ICategoryService categoryService, 
            IFeedbackService feedbackService,
            ICurrentUserService currentUserService)
        {
            this.mapper = mapper;
            this.placeService = placeService;
            this.categoryService = categoryService;
            this.feedbackService = feedbackService;
            this.currentUserService = currentUserService;
        }

        [Route(nameof(Details))]
        public async Task<IActionResult> Details(int Id)
        {
            var place = await this.placeService.GetByIdAsync(Id);
            return View("Details", place);
        }

        [Authorize]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId; 

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetAsync());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            //List<CountryModel> countryList = new List<CountryModel>(await countryService.GetAsync());
            //countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            //ViewBag.ListOfCountry = countryList;

            return View();
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Create))]
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
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(int Id)
        {
            var place = await placeService.GetByIdAsync(Id);

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetAsync());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            //List<CountryModel> countryList = new List<CountryModel>(await countryService.GetAsync());
            //countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            //ViewBag.ListOfCountry = countryList;

            //List<CityModel> cityList = new List<CityModel>(await cityService.GetCitiesByCountryAsync(place.CountryId));
            //cityList.Insert(0, new CityModel { Id = 0, Name = "Select" });
            //ViewBag.ListOfCities = cityList;

            return View(mapper.Map<PlaceSetModel>(place));
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Edit))]
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
        [Route(nameof(Delete))]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        [Route(nameof(ConfirmDelete))]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.placeService.DeleteAsync(Id);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [Route(nameof(ByCategory))]
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = categoryService.GetCategoryName(categoryId);

            var places = await placeService.GetByCategoryAsync(categoryId);
            return View("List", places);
        }

        [Authorize]
        [Route(nameof(MyPlaces))]
        public async Task<IActionResult> MyPlaces()
        {
            var places = await this.placeService.GetByUserAsync(this.currentUserService.UserId);

            ViewBag.UserName = this.currentUserService.FirstName;
            return View("List", places);
        }

        [HttpPost]
        [Route(nameof(SearchPlaces))]
        public async Task<IActionResult> SearchPlaces(string search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var foundPlaces = await placeService.SearchPlacesAsync(search);
                return View("List", foundPlaces);
            }

            var places = await placeService.AllAysnc();
            return View("List", places);
        }

        [Authorize]
        [Route(nameof(All))]
        public async Task<IActionResult> All()
        {
            var places = await this.placeService.AllAysnc();
            return View("List", places);
        }

        [Route(nameof(GetCities))]
        public async Task<JsonResult> GetCities(int countryId)
        {
            //var cityList = new List<CityModel>(await cityService.GetCitiesByCountryAsync(countryId));
            //cityList.Insert(0, new CityModel { Id = 0, Name = "Select" });
            var cityList = new List<string>();

            return Json(new SelectList(cityList, "Id", "Name"));
        }

        [Route(nameof(AddFeedback))]
        public IActionResult AddFeedback(int placeId)
        {
            var model = new FeedbackSetModel { PlaceId = placeId };
            return View(model);
        }

        [HttpPost]
        [Route(nameof(AddFeedback))]
        public async Task<IActionResult> AddFeedback(FeedbackSetModel feedbackModel)
        {
            if (this.ModelState.IsValid)
            {
                var feedback = mapper.Map<Feedback>(feedbackModel);
                feedback.Date = DateTime.Now;
                if (this.currentUserService != null)
                {
                    feedback.UserId = this.currentUserService.UserId;
                }
                await this.feedbackService.CreateAsync(feedback);
                return RedirectToAction(nameof(PlaceController.Details), new { Id = feedbackModel.PlaceId});
            }

            return View(feedbackModel);
        }
    }
}
