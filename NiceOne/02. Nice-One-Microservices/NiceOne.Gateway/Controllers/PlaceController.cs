namespace NiceOne.Gateway.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Gateway.Models.Places;
    using NiceOne.Gateway.Services.Identity;
    using NiceOne.Gateway.Services.Place;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaceController : ApiController
    {
        private readonly IIdentityService identityService;
        private readonly IPlaceService placeService;

        public PlaceController(IIdentityService identityService, IPlaceService placeService)
        {
            this.identityService = identityService;
            this.placeService = placeService;
        }

        [HttpGet]
        [Route(nameof(Details) + PathSeparator + Id)]
        public async Task<PlaceGetModel> Details(int id)
        {
            var place = await this
                    .placeService
                    .GetPlace(id);

            var userIds = place.Feedbacks
                        .Where(f => f.UserId != null)
                        .Select(f => f.UserId);

            var userNames = await this
                    .identityService
                    .UserNames(userIds);

            var userNamesDictionary = userNames.ToDictionary(u => u.UserId, u => u.UserName);


            foreach(var feedback in place.Feedbacks)
            {
                feedback.User = feedback.UserId == null ? "Anonymous" : userNamesDictionary[feedback.UserId];
            }

            return place;
        }
    }
}
