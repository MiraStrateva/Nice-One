namespace NiceOne.Place.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Place.Models.Feedbacks;
    using NiceOne.Place.Services.Feedbacks;
    using NiceOne.Services.Identity;
    using System;
    using System.Threading.Tasks;

    public class FeedbackController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IFeedbackService feedbackService;
        private readonly ICurrentUserService currentUserService;

        public FeedbackController(IMapper mapper,
            IFeedbackService feedbackService, 
            ICurrentUserService currentUserService)
        {
            this.mapper = mapper;
            this.feedbackService = feedbackService;
            this.currentUserService = currentUserService;
        }

        //[Authorize]
        [Route(nameof(AllByUser))]
        public async Task<IActionResult> AllByUser()
            => Ok(await feedbackService.GetByUserAsync(this.currentUserService.UserId));

        [Route(nameof(AllByPlace) + PathSeparator + Id)]
        public async Task<IActionResult> AllByPlace(int id)
            => Ok(await feedbackService.GetByPlaceAsync(id));

        [HttpPost]
        //[Authorize]
        [Route(nameof(Edit) + PathSeparator + Id)]
        public async Task<IActionResult> Edit(FeedbackSetModel feedbackModel)
        {
            if (this.ModelState.IsValid)
            {
                var feedback = await this.feedbackService.FindAsync(feedbackModel.Id);

                feedback.Text = feedbackModel.Text;
                feedback.Rating = feedbackModel.Rating;
                feedback.Date = DateTime.Now;

                await this.feedbackService.SaveAsync(feedback);
                return Ok();
            }

            return BadRequest("Model is not valid.");
        }

        //[Authorize]
        [Route(nameof(ConfirmDelete) + PathSeparator + Id)]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.feedbackService.DeleteAsync(Id);
            return Ok();
        }
    }
}
