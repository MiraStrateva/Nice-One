namespace NiceOne.Notifications.Messages
{
    using System.Threading.Tasks;
    using NiceOne.Messages.Place;
    using Hub;
    using MassTransit;
    using Microsoft.AspNetCore.SignalR;

    using static NiceOne.Constants;
    using static Constants;

    public class FeedbackCreatedConsumer : IConsumer<FeedbackCreatedMessage>
    {
        private readonly IHubContext<NotificationsHub> hub;

        public FeedbackCreatedConsumer(IHubContext<NotificationsHub> hub)
            => this.hub = hub;

        public async Task Consume(ConsumeContext<FeedbackCreatedMessage> context)
            => await this.hub
                .Clients
                //.Groups(AuthenticatedUsersGroup)
                .All
                .SendAsync(ReceiveNotificationEndpoint, string.Concat(context.Message.Rating, ": ", context.Message.Text));
    }
}
