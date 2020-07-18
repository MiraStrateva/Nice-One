namespace NiceOne.Client.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using NiceOne.Client.Models.Location;
    using NiceOne.Client.Models.Place.Categories;
    using NiceOne.Client.Models.Place.Feedbacks;
    using NiceOne.Client.Models.Place.Places;
    using NiceOne.Client.Services.Gateway;
    using NiceOne.Client.Services.Location;
    using NiceOne.Client.Services.Place;
    using NiceOne.Infrastructure;
    using NiceOne.Place.Services.Categories;
    using NiceOne.Services.Identity;

    public class PlaceController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IPlaceService placeService;
        private readonly ICategoryService categoryService;
        private readonly ILocationService locationService;
        private readonly ICurrentUserService currentUserService;
        private readonly IGatewayService gatewayService;

        public PlaceController(IMapper mapper,
            IPlaceService placeService, 
            ICategoryService categoryService,
            ILocationService locationService,
            ICurrentUserService currentUserService, 
            IGatewayService gatewayService)
        {
            this.mapper = mapper;
            this.placeService = placeService;
            this.categoryService = categoryService;
            this.locationService = locationService;
            this.currentUserService = currentUserService;
            this.gatewayService = gatewayService;
        }

        public async Task<IActionResult> Details(int Id)
            => View(await this.gatewayService.Details(Id));

        [Authorize]
        public async Task<IActionResult> Create(int categoryId)
        {
            ViewBag.CategoryId = categoryId; 

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetCategories());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            List<CountryModel> countryList = new List<CountryModel>(await locationService.GetCountries());
            countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PlaceSetModel input)
            => await this.Handle(
                async () => await this.placeService.Create(input),
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: View(input));

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
        {
            var place = await gatewayService.Details(Id);

            List<CategoryGetModel> categotyList = new List<CategoryGetModel>(await categoryService.GetCategories());
            categotyList.Insert(0, new CategoryGetModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCategory = categotyList;

            List<CountryModel> countryList = new List<CountryModel>(await locationService.GetCountries());
            countryList.Insert(0, new CountryModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCountry = countryList;

            List<CityModel> cityList = new List<CityModel>(await locationService.GetCities(place.CountryId));
            cityList.Insert(0, new CityModel { Id = 0, Name = "Select" });
            ViewBag.ListOfCities = cityList;

            return View(mapper.Map<PlaceSetModel>(place));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, PlaceSetModel input)
            => await this.Handle(
                async () => await this.placeService.Edit(id, input),
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: View(input));

        [Authorize]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int Id)
            => await this.Handle(
                async () => await this.placeService.Delete(Id),
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: RedirectToAction(nameof(HomeController.Index), "Home"));

        public async Task<IActionResult> ByCategory(int id)
        {
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = await categoryService.GetCategoryName(id);

            var places = await placeService.ByCategory(id);
            return View("List", places);
        }

        [Authorize]
        public async Task<IActionResult> MyPlaces()
        {
            var places = await this.placeService.MyPlaces();

            ViewBag.UserName = this.currentUserService.FirstName;
            return View("List", places);
        }

        [HttpPost]
        public async Task<IActionResult> SearchPlaces(string search = null)
            => View("List", await this.placeService.SearchPlaces(search));

        [AuthorizeAdministrator]
        public async Task<IActionResult> All()
            => View("List", await this.placeService.All());

        public async Task<JsonResult> GetCities(int countryId)
        {
            var cityList = new List<CityModel>(await locationService.GetCities(countryId));
            cityList.Insert(0, new CityModel { Id = 0, Name = "Select" });
            
            return Json(new SelectList(cityList, "Id", "Name"));
        }

        public IActionResult AddFeedback(int id)
        {
            var model = new FeedbackSetModel { PlaceId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(FeedbackSetModel input)
            => await this.Handle(
                async () => await this.placeService.AddFeedback(input),
                success: RedirectToAction(nameof(PlaceController.Details), new { Id = input.PlaceId }),
                failure: View(input));
    }
}
