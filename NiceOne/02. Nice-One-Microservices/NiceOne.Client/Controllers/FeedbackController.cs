namespace NiceOne.Client.Controllers
{
    using System.Threading.Tasks;
    
    using AutoMapper;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Client.Models.Place.Feedbacks;
    using NiceOne.Services.Identity;
    using NiceOne.Client.Services.Place;

    public class FeedbackController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IPlaceService placeService;

        public FeedbackController(IMapper mapper,
            IPlaceService service)
        {
            this.mapper = mapper;
            this.placeService = service;
        }

        [Authorize]
        public async Task<IActionResult> AllByUser()
            => View("List", await this.placeService.GetFeedbacksForCurrentUser());

        [Authorize]
        public async Task<IActionResult> Edit(int Id)
            => View(await this.placeService.GetFeedback(Id));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(FeedbackSetModel input)
            => await this.Handle(
                async () => await this.placeService.EditFeedback(input.Id, input),
                success: RedirectToAction(nameof(FeedbackController.AllByUser)),
                failure: View(input));

        [Authorize]
        public IActionResult Delete(int id)
        {
            return this.View(id);
        }

        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int Id)
        {
            await this.placeService.DeleteFeedback(Id);
            return RedirectToAction(nameof(FeedbackController.AllByUser));
        }
    }
}
