namespace Workshop.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    using Workshop.Data.Models;
    using Workshop.Services.Data.HashProvider;
    using Workshop.Services.Data.Messages;
    using Workshop.Services.Data.Notifications;
    using Workshop.Services.InputModels.Messages;
    using Workshop.Services.InputModels.Notifications;
    using Workshop.Web.ViewModels.Chat;
    using Workshop.Web.ViewModels.Notifications;

    [Authorize]
    public class ChatHub : Hub
    {
        private IHashProvider hashProvider;
        private IHubContext<NotificationHub> hubContext;
        private INotificationsService notificationsService;
        private IMessagesService messagesService;
        private UserManager<ApplicationUser> userManager;

        public ChatHub(
            IHashProvider hashProvider,
            IHubContext<NotificationHub> hubContext,
            INotificationsService notificationsService,
            IMessagesService messagesService,
            UserManager<ApplicationUser> userManager)
        {
            this.hashProvider = hashProvider;
            this.hubContext = hubContext;
            this.notificationsService = notificationsService;
            this.messagesService = messagesService;
            this.userManager = userManager;
        }

        public async Task Send(string id, string messageContent)
        {
            var currUser = await this.userManager.GetUserAsync(this.Context.User);

            var messageFordb = new MessageCreateInputModel()
            {
                Content = messageContent,
                SenderId = currUser.Id,
                ReceiverId = id,
            };
            await this.messagesService.CreateAsync(messageFordb);

            var hashGroup = this.hashProvider.HashOfGivenStrings(id, currUser.Id);
            var message = new MessageViewModel()
            {
                SenderUserName = currUser.UserName,
                SenderAvatarUrl = currUser.AvatarUrl,
                Content = messageContent,
                CreatedOn = DateTime.UtcNow.ToString(),
            };
            await this.Clients.Group(hashGroup).SendAsync("NewMessage", message);

            var notificationMessage = new NotificationViewModel()
            {
                SenderUserName = this.Context.User.Identity.Name,
                SenderAvatarUrl = null,
                Description = messageContent,
                Link = $"/Chat/Private/{currUser.Id}",
            };
            var notif = new NotificationCreateInputModel()
            {
                SenderId = currUser.Id,
                ReceiverId = id,
                Description = messageContent,
            };
            await this.notificationsService.CreateAsync(notif);
            await this.hubContext.Clients.User(id).SendAsync("NewNotification", notificationMessage);
        }

        public async Task Add(string id)
        {
            var currUser = this.Context.UserIdentifier;
            var hashGroup = this.hashProvider.HashOfGivenStrings(id, currUser);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, hashGroup);
        }
    }
}
