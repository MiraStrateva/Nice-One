namespace NiceOne.Place.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Infrastructure;
    using NiceOne.Place.Data.Entities;
    using NiceOne.Place.Models.Feedbacks;
    using NiceOne.Place.Models.Places;
    using NiceOne.Place.Services.Categories;
    using NiceOne.Place.Services.Feedbacks;
    using NiceOne.Place.Services.Places;
    using NiceOne.Services.Identity;
    using System;
    using System.Collections.Generic;
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

        [Route(nameof(Details) + PathSeparator + Id)]
        public async Task<IActionResult> Details(int Id)
            => Ok(await this.placeService.GetByIdAsync(Id));

        [HttpPost]
        [Authorize]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create(PlaceSetModel placeSetModel)
        {
            if (this.ModelState.IsValid)
            {
                var place = mapper.Map<Place>(placeSetModel);
                await this.placeService.CreateAsync(place);
                return Ok();
            }

            return BadRequest("Model is not valid");
        }

        [HttpPost]
        //[Authorize]
        [Route(nameof(Edit) + PathSeparator + Id)]
        public async Task<IActionResult> Edit(int id, PlaceSetModel placeSetModel)
        {
            if (this.ModelState.IsValid)
            {
                var place = await this.placeService.FindAsync(id);

                place.Name = placeSetModel.Name;
                place.Description = placeSetModel.Description;
                place.CategoryId = placeSetModel.CategoryId;
                place.CityId = placeSetModel.CityId;
                place.CityName = placeSetModel.CityName;
                place.CountryId = placeSetModel.CountryId;
                place.CountryName = placeSetModel.CountryName;

                await this.placeService.SaveAsync(place);
                return Ok();
            }

            return BadRequest("Model is not valid");
        }

        //[Authorize]
        [Route(nameof(ConfirmDelete) + PathSeparator + Id)]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.placeService.DeleteAsync(Id);
            return Ok();
        }

        [Route(nameof(ByCategory) + PathSeparator + Id)]
        public async Task<IActionResult> ByCategory(int id)
            => Ok(await placeService.GetByCategoryAsync(id));

        [Authorize]
        [Route(nameof(MyPlaces))]
        public async Task<IActionResult> MyPlaces()
            => Ok(await this.placeService.GetByUserAsync(this.currentUserService.UserId));

        [HttpPost]
        [Route(nameof(SearchPlaces))]
        public async Task<IActionResult> SearchPlaces(string search = null)
        {
            IEnumerable<PlaceListGetModel> places = default;
            
            if (!string.IsNullOrEmpty(search))
            {
                places = await placeService.SearchPlacesAsync(search);
            }

            places = await placeService.AllAysnc();

            return Ok(places);
        }

        [AuthorizeAdministrator]
        [Route(nameof(All))]
        public async Task<IActionResult> All()
            => Ok(await this.placeService.AllAysnc());

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
                return Ok();
            }

            return BadRequest("Model is not valid"); 
        }
    }
}
