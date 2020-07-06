namespace NiceOne.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.DTOs.Feedbacks;
    using NiceOne.Services.Feedbacks;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class FeedbackController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFeedbackService feedbackService;
        public FeedbackController(IMapper mapper,
            IFeedbackService feedbackService)
        {
            this.mapper = mapper;
            this.feedbackService = feedbackService;
        }

        [Authorize]
        public async Task<IActionResult> AllByUser()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var feedbacks = await feedbackService.GetByUserAsync(userId);
            return View("List", feedbacks);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
        {
            var feedback = await feedbackService.GetByIdAsync(Id);

            return View(mapper.Map<FeedbackSetModel>(feedback));
        }

        [HttpPost]
        [Authorize]
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
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.feedbackService.DeleteAsync(Id);
            return RedirectToAction(nameof(FeedbackController.AllByUser));
        }
    }
}
