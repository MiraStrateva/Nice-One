namespace NiceOne.Place.Messages
{
    using MassTransit;
    using NiceOne.Messages.Location;
    using NiceOne.Place.Services.Places;
    using System.Threading.Tasks;

    public class CityUpdatedConsumer : IConsumer<CityUpdatedMessage>
    {
        private readonly IPlaceService placeService;

        public CityUpdatedConsumer(IPlaceService service)
        {
            this.placeService = service;
        }

        public async Task Consume(ConsumeContext<CityUpdatedMessage> context) 
            => await placeService.UpdateCityName(context.Message);
    }
}
