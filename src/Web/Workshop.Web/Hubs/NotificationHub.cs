namespace Workshop.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using Workshop.Services.Data.NotificationsUsersStatusCollection;

    [Authorize]
    public class NotificationHub : Hub
    {
        private INotificationUsersStatusCollection usersStatusCollection;

        public NotificationHub(INotificationUsersStatusCollection usersStatusCollection)
        {
            this.usersStatusCollection = usersStatusCollection;
        }

        public override async Task OnConnectedAsync()
        {
            var currUserName = this.Context.User.Identity.Name;
            if (!this.usersStatusCollection.IsInCollection(currUserName))
            {
                this.usersStatusCollection.Add(currUserName);
                this.usersStatusCollection.Active(currUserName);
            }
            else
            {
                this.usersStatusCollection.Active(currUserName);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var currUserName = this.Context.User.Identity.Name;
            this.usersStatusCollection.NonActive(currUserName);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task Send()
        {
            var message = "test";

            await this.Clients.All.SendAsync("NewNotification", message);
        }
    }
}
