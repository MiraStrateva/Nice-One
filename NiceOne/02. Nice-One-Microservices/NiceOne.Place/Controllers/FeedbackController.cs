namespace NiceOne.Place.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Place.DTOs.Feedbacks;
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

        [Authorize]
        [Route(nameof(AllByUser))]
        public async Task<IActionResult> AllByUser()
        {
            var feedbacks = await feedbackService.GetByUserAsync(this.currentUserService.UserId);
            return View("List", feedbacks);
        }

        [Authorize]
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(int Id)
        {
            var feedback = await feedbackService.GetByIdAsync(Id);

            return View(mapper.Map<FeedbackSetModel>(feedback));
        }

        [HttpPost]
        [Authorize]
        [Route(nameof(Edit))]
        public async Task<IActionResult> Edit(FeedbackSetModel feedbackModel)
        {
            if (this.ModelState.IsValid)
            {
                var feedback = await this.feedbackService.FindAsync(feedbackModel.Id);

                feedback.Text = feedbackModel.Text;
                feedback.Rating = feedbackModel.Rating;
                feedback.Date = DateTime.Now;

                await this.feedbackService.SaveAsync(feedback);
                return RedirectToAction(nameof(FeedbackController.AllByUser));
            }

            return View(feedbackModel);
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
            await this.feedbackService.DeleteAsync(Id);
            return RedirectToAction(nameof(FeedbackController.AllByUser));
        }
    }
}
