namespace NiceOne.Place.Messages
{
    using MassTransit;
    using NiceOne.Messages.Location;
    using NiceOne.Place.Services.Places;
    using System.Threading.Tasks;

    public class CountryUpdatedConsumer : IConsumer<CountryUpdatedMessage>
    {
        private readonly IPlaceService placeService;

        public CountryUpdatedConsumer(IPlaceService placeService)
        {
            this.placeService = placeService;
        }

        public async Task Consume(ConsumeContext<CountryUpdatedMessage> context)
            => await placeService.UpdateCountryName(context.Message);
    }
}
