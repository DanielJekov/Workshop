namespace Workshop.Web.Hubs
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    using Workshop.Services.Data.ActivityUsersStatusCollection;

    [Authorize]
    public class NotificationHub : Hub
    {
        private IActivityUsersStatusCollection usersStatusCollection;

        public NotificationHub(IActivityUsersStatusCollection usersStatusCollection)
        {
            this.usersStatusCollection = usersStatusCollection;
        }

        public override async Task OnConnectedAsync()
        {
            var currUserId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!this.usersStatusCollection.IsInCollection(currUserId))
            {
                this.usersStatusCollection.Add(currUserId);
                this.usersStatusCollection.Active(currUserId);
            }
            else
            {
                this.usersStatusCollection.Active(currUserId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var currUserId = this.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.usersStatusCollection.NonActive(currUserId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
