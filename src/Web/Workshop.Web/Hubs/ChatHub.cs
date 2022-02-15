namespace Workshop.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using Workshop.Services.Data;
    using Workshop.Web.ViewModels.Chat;
    using Workshop.Web.ViewModels.Notifications;

    [Authorize]
    public class ChatHub : Hub
    {
        private IHashProvider hashProvider;
        private IHubContext<NotificationHub> hubContext;

        public ChatHub(IHashProvider hashProvider, IHubContext<NotificationHub> hubContext)
        {
            this.hashProvider = hashProvider;
            this.hubContext = hubContext;
        }

        public async Task Send(string id, string messageInput)
        {
            var currUser = this.Context.UserIdentifier;
            var hashGroup = this.hashProvider.Hash(id, currUser);

            var message = new ChatMessageViewModel()
            {
                Username = this.Context.User.Identity.Name,
                MessageText = messageInput,
                CreatedOn = string.Concat("Today, ", DateTime.Now.ToString("HH:mm")),
            };

            await this.Clients.Group(hashGroup).SendAsync("NewMessage", message);

            var notificationMessage = new NotificationViewModel()
            {
                Sender = this.Context.User.Identity.Name,
                Message = messageInput,
                Link = $"/Chat/Private/{currUser}",
            };

            await this.hubContext.Clients.User(id).SendAsync("NewNotification", notificationMessage);
        }

        public async Task Add(string id)
        {
            var currUser = this.Context.UserIdentifier;
            var hashGroup = this.hashProvider.Hash(id, currUser);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, hashGroup);
        }
    }
}
