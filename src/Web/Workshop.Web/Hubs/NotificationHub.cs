namespace Workshop.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub
    {
        public async Task Send()
        {
            var message = "test";

            await this.Clients.All.SendAsync("NewNotification", message);
        }
    }
}
